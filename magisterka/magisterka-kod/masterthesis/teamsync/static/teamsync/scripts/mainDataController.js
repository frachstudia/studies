(function() {
    var app = angular.module('teamSyncApp', []);
    app.config(
        ['$httpProvider', function($httpProvider) {
            $httpProvider.defaults.xsrfCookieName = 'csrftoken';
            $httpProvider.defaults.xsrfHeaderName = 'X-CSRFToken';
        }]
    );

    app.controller('mainDataController', ['$anchorScroll', '$location', '$window', '$scope', '$http', '$filter', function($anchorScroll, $location, $window, $scope, $http, $filter) {
        $scope.avatarPath = '/teamsync/static/teamsync/img/avatar.png';
        $scope.uid = '';
        var orderBy = $filter('orderBy');

        // Getting folder and file lists
        refreshFolders = function() {
            $http.get('/getfolders').then(
                function (response) {
                    $scope.folders = response.data;
                }
            );
        };
        refreshFiles = function() {
            $http.post('/getfiles', {'insidepath': $scope.currentPath, 'folderpath': $scope.currentFolder.dir}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function (response) {
                    $scope.files = response.data;
                    $scope.loadingFiles = false;

                    if ($scope.viewMode === 'wątki') {
                        if ($scope.threadSortingType === 'none')        return;
                        var sortType = 'name';
                        var sortDiretion = false;
                        if ($scope.threadSortingType === '1')           sortType = 'timestamp';
                        if ($scope.threadSortingType === '2')           sortType = 'numberofcomments';
                        if ($scope.threadSortingType === '3')           sortType = 'lastcomment';
                        if ($scope.threadSortingDirection === '1')      sortDirection = true;

                        $scope.files = orderBy($scope.files, sortType, sortDirection);
                    }
                }
            );
            for (var i = 0; i < $scope.currentFolder.users.length; i++) {
                $scope.UIDtoIdentity[$scope.currentFolder.users[i].uid] = $scope.currentFolder.users[i].identity;
            }
        };
        refreshConfig = function() {
            $http.get('/getconfig').then(
                function (response) {
                    $scope.threadSortingType = response.data.threadsorting1;
                    $scope.threadSortingDirection = response.data.threadsorting2;
                    $scope.uid = response.data.uid;
                    $scope.defaultPath = response.data.defaultpath;
                }
            );
        };
        getAllComments = function() {
            $http.post('/getallcomments', {'path': $scope.currentFolder.dir}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    $scope.comments = response.data.comments;
                    $scope.stats = response.data.stats;
                }
            );
        };
        getCommentsFromPath = function() {
            $http.post('/getcommentsfrompath', {'folderpath': $scope.currentFolder.dir, 'insidepath': $scope.currentPath }).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    $scope.comments = response.data.comments;
                    $scope.stats = response.data.stats;
                }
            );
        };

        // ############################################## NAVIGATION ###################################################

        makeAllInvisible = function() {
            $scope.newFolderWindowVisible = false;
            $scope.sortThreadsMenuVisible = false;
            $scope.dominatingUsersMenuVisible = false;
            $scope.globalCommentsMenuVisible = false;
            $scope.userViewMenuVisible = false;
            $scope.searchCommentsVisible = false;
        };

        // ############################################ SIDEBAR #######################################################

        // ARRAYS, DICTIONARIES, ETC.
        $scope.folders = [];
        $scope.currentFolder = {};
        $scope.items = [];

        // STRINGS
        $scope.newFolderPath = '';
        $scope.newFolderSecret = '';
        $scope.newFolderIdentity = '';
        $scope.defaultPath = '';
        $scope.numberOfFiles = 0;

        // BOOLEANS
        $scope.folderChosen = false;
        $scope.newFolderWindowVisible = false;
        $scope.newExistingFolderWindowVisible = false;
        $scope.directoryPickerWindowVisible = false;

        // INITIALIZATION
        refreshFolders();
        refreshConfig();

        $scope.newFolderButtonClicked = function() {
            $scope.newFolderWindowVisible = true;
            $scope.newFolderSecret = 'eee';
            $scope.newFolderIdentity = '';
            $scope.newFolderPath = $scope.defaultPath;
            $scope.chooseFolderChooser('home', '');
        };
        $scope.newExistingFolderButtonClicked = function() {
            $scope.newExistingFolderWindowVisible = true;
            $scope.newFolderIdentity = '';
            $scope.newFolderPath = $scope.defaultPath;
            $scope.chooseFolderChooser('home', '');
        };
        $scope.directoryPickerButtonClicked = function() {
            $scope.directoryPickerWindowVisible = true;
        };

        $scope.chooseFolderChooser = function(move, folder) {
            $http.post('/getitems', {'path': $scope.newFolderPath, 'folder': folder, 'move': move}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    $scope.items = response.data.data;
                    $scope.newFolderPath = response.data.path;
                    $scope.numberOfFiles = response.data.numberoffiles;
                }
            );
        };

        $scope.chooseFolder = function() {
            makeAllInvisible();
            $scope.viewMode = 'pliki';
            $scope.folderChosen = true;
            $scope.commentsHeaderVisible = false;

            $scope.currentThread = {'name': ''};
            $scope.comments = [];
            $scope.stats = {};
            $scope.newThreadVisible = false;
            $scope.currentPath = '/';
            refreshFiles();
            $scope.userViewIconVisible = false;
            $scope.searchCommentsIconVisible = false;
            $scope.searchCommentsVisible = false;
            $scope.statisticsIconVisible = false;
            $scope.newThreadOptionDisabled = false;
            $scope.statisticsVisible = false;

            // Setting dominatingUsers dictionary
            $scope.dominatingUsers = [];
            for (var i = 0; i < $scope.currentFolder.users.length; i++) {
                $scope.dominatingUsers.push( {'uid': $scope.currentFolder.users[i].uid, 'identity': $scope.currentFolder.users[i].identity, 'isDominating': false } );
            }
        };
        $scope.addNewFolder = function(secret) {
            $http.post('/addfolder', {
                'path': $scope.newFolderPath,
                'identity': $scope.newFolderIdentity,
                'secret': secret})
            .success(function (data) {
                $.notify('Dodano folder.', {className: 'success', position: 'right bottom'});
                refreshFolders();
                $scope.newFolderWindowVisible = false;
                $scope.newExistingFolderWindowVisible = false;
                $scope.folderChosen = false;
            }).error(function (data) {
                $.notify(data, {className: 'error', position: 'right bottom'});
            });
        };
        $scope.deleteFolder = function() {
            $http.post('/deletefolder', {'secret': $scope.currentFolder.secret})
            .success(function(data) {
                $.notify('Usunięto folder ' + $scope.currentFolder.name + '.', {className: 'success', position: 'right bottom'});
                refreshFolders();
                $scope.currentFolder = {};
                $scope.folderChosen = false;
            }).error(function(data) {
                $.notify(data, {className: 'error', position: 'right bottom'});
            });
        };

        // ######################################## THREADS HEADER ####################################################

        // ARRAYS, DICTIONARIES, ETC.
        $scope.currentThread = {'name': ''};
        $scope.dominatingUsers = [];
        $scope.rightFileNameAreaValue = '0px';

        // STRINGS
        $scope.currentPath = '/';
        $scope.viewMode = 'pliki';
        $scope.threadSortingType = '10';
        $scope.threadSortingDirection = '10';
        $scope.threshold = '< 10 %';

        // BOOLEANS
        $scope.showOnlyThreads = false;
        $scope.sortThreadsMenuVisible = false;
        $scope.dominatingUsersMenuVisible = false;

        $scope.sortThreadsButtonClicked = function() {
            if (!$scope.sortThreadsMenuVisible) {
                makeAllInvisible();
                $scope.sortThreadsMenuVisible = true;
            } else {
                $scope.sortThreadsMenuVisible = false;
            }
        };
        $scope.dominatingUsersButtonClicked = function() {
            if (!$scope.dominatingUsersMenuVisible) {
                makeAllInvisible();
                $scope.dominatingUsersMenuVisible = true;
            } else {
                $scope.dominatingUsersMenuVisible = false;
            }
        };
        $scope.sendDominatingUsers = function() {
            $http.post('/getdominatedthreads', {'users': $scope.dominatingUsers, 'threshold': $scope.threshold, 'path': $scope.currentFolder.dir }).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function (response) {
                    $scope.files = response.data;
                }
            );
            $scope.dominatingUsersButtonClicked();
        };
        $scope.resetDominatingUsers = function() {
            for (var i = 0; i < $scope.dominatingUsers.length; i++) {
                $scope.dominatingUsers[i].isDominating = false;
            }
            $scope.threshold = '50 %';

            refreshFiles();
            $scope.dominatingUsersButtonClicked();
        };

        $scope.backPath = function() {
            if ($scope.viewMode === 'pliki') {
                var tempPath = $scope.currentPath.substring(0, $scope.currentPath.lastIndexOf("/"));
                if (tempPath === '')  tempPath = '/';
                $scope.currentPath = tempPath;
            }
        };
        $scope.homePath = function() {
            if ($scope.viewMode === 'pliki') {
                $scope.currentPath = '/';
            }
        };
        $scope.$watch("currentPath", function() {
            if ($scope.folderChosen === false)          return;
            if ($scope.currentCommentsScope === '2')    getCommentsFromPath();

            $scope.files = [];
            $scope.loadingFiles = true;
            refreshFiles();
        });
        $scope.$watch("viewMode", function() {
            makeAllInvisible();

            if ($scope.viewMode === 'pliki') {
                $scope.currentPath = '/';
            } else {
                $scope.currentPath = '';

                if ($scope.threadSortingType === 'none')        return;
                var sortType = 'name';
                var ascending = false;
                if ($scope.threadSortingType === '1')           sortType = 'timestamp';
                if ($scope.threadSortingType === '2')           sortType = 'numberofcomments';
                if ($scope.threadSortingType === '3')           sortType = 'lastcomment';
                if ($scope.threadSortingDirection === '1')      ascending = true;

                $scope.files = orderBy($scope.files, sortType, ascending);
            }
        });
        $scope.$watch("threadSortingType", function() {
            if ($scope.threadSortingType === 'none')        return;
            var sortType = 'name';
            var ascending = false;
            if ($scope.threadSortingType === '1')           sortType = 'timestamp';
            if ($scope.threadSortingType === '2')           sortType = 'numberofcomments';
            if ($scope.threadSortingType === '3')           sortType = 'lastcomment';
            if ($scope.threadSortingDirection === '1')      ascending = true;

            $scope.files = orderBy($scope.files, sortType, ascending);
        });
        $scope.$watch("threadSortingDirection", function() {
            if ($scope.threadSortingType === 'none')        return;
            var sortType = 'name';
            var ascending = false;
            if ($scope.threadSortingType === '1')           sortType = 'timestamp';
            if ($scope.threadSortingType === '2')           sortType = 'numberofcomments';
            if ($scope.threadSortingType === '3')           sortType = 'lastcomment';
            if ($scope.threadSortingDirection === '1')      ascending = true;

            $scope.files = orderBy($scope.files, sortType, ascending);
        });

        $scope.isTimestampSorted = function() {
            if ($scope.viewMode === 'pliki') {
                $scope.rightFileNameAreaValue = '0px';
                return false;
            }

            if ($scope.threadSortingType === '1') {
                $scope.rightFileNameAreaValue = '115px';
                return true;
            }

            return false;
        };
        $scope.isPopularitySorted = function() {
            if ($scope.viewMode === 'pliki') {
                $scope.rightFileNameAreaValue = '0px';
                return false;
            }

            if ($scope.threadSortingType === '2') {
                $scope.rightFileNameAreaValue = '125px';
                return true;
            }

            return false;
        };
        $scope.isFreshnessSorted = function() {
            if ($scope.viewMode === 'pliki') {
                $scope.rightFileNameAreaValue = '0px';
                return false;
            }

            if ($scope.threadSortingType === '3') {
                $scope.rightFileNameAreaValue = '115px';
                return true;
            }

            return false;
        };

        $scope.changeView = function(change) {
            if ($scope.viewMode === 'pliki') {
                $scope.viewMode = 'wątki';
            } else {
                $scope.viewMode = 'pliki';
            }
        };

        // ########################################  THREADS BODY  ####################################################

        $scope.loadingFiles = false;

        // ARRAYS, DICTIONARIES, ETC.
        $scope.files = [];

        $scope.isFilesLoading = function() {
            return $scope.loadingFiles;
        };
        $scope.showThreadFile = function(file) {
            if (file.type != 'file')
                return 0;

            if (file.threads.length > 0)
                return 1;
            else
                return 2;
        };
        $scope.newThread = function(fileAbout) {
            $scope.newThreadPath = $scope.currentPath;
            if (fileAbout === '')
                $scope.newThreadFile = '<brak>';
            else
                $scope.newThreadFile = fileAbout;
            $scope.newThreadComment = '';
            $scope.currentCommentsScope = '0';
            $scope.changeCommentsScope();
        };
        $scope.fileClicked = function(file) {
            switch (file.type) {
                case 'folder':
                    $scope.currentPath = file.insidepath;
                    break;
                case 'file':
                    file.unrolled = !file.unrolled;
                    break;
                default:
                    $scope.threadClicked(file);
                    break;
            }
        };
        $scope.threadClicked = function(file) {
            $scope.searchphrase = '';
            $scope.currentThread = file;
            if ('topic' in file)
                $scope.currentThread.name = file.topic;
            refreshComments('');
            $scope.currentCommentsScope = '1';
            $scope.changeCommentsScope();
        };
        $scope.showODP = function(file) {
            if (file.type === 'file') {
                // if any thread is unread, return true, otherwise false
                for (var i in file.threads) {
                    if (file.threads[i].unreadcomment)
                        return true;
                }
                return false;
            } else {
                if (file.unreadcomment)
                    return true;
                else
                    return false;
            }
        };

        // ####################################### COMMENTS HEADER ####################################################

        // STRINGS
        $scope.currentCommentsScope = -1;
        $scope.searchphrase = {'comment': ''};
        $scope.commentsHeaderText = 'Tytuł wątku:';
        $scope.sortedByUser = 'NTP (globalna spójność)';
        $scope.commentsOrder = 'timestamp';

        // BOOLEANS
        $scope.commentsHeaderVisible = false;
        $scope.globalCommentsMenuVisible = false;
        $scope.userViewIconVisible = false;
        $scope.userViewMenuVisible = false;
        $scope.searchCommentsVisible = false;
        $scope.searchCommentsIconVisible = false;
        $scope.statisticsVisible = false;
        $scope.statisticsIconVisible = false;
        $scope.newThreadOptionDisabled = true;
        $scope.currentThreadOptionDisabled = true;

        $scope.globalCommentsMenuClicked = function() {
            if (!$scope.globalCommentsMenuVisible) {
                makeAllInvisible();
                $scope.globalCommentsMenuVisible = true;
            } else {
                $scope.globalCommentsMenuVisible = false;
            }
        };
        $scope.userViewMenuClicked = function() {
            if (!$scope.userViewMenuVisible) {
                makeAllInvisible();
                $scope.userViewMenuVisible = true;
            } else {
                $scope.userViewMenuVisible = false;
            }
        };
        $scope.searchCommentsClicked = function() {
            $scope.searchphrase = {'comment': ''};

            if (!$scope.searchCommentsVisible) {
                makeAllInvisible();
                $scope.searchCommentsVisible = true;
            } else {
                $scope.searchCommentsVisible = false;
            }
        };
        $scope.changeCommentsScope = function() {
            makeAllInvisible();
            $scope.commentsHeaderVisible = true;
            $scope.newThreadOptionDisabled = true;
            $scope.currentThreadOptionDisabled = true;
            $scope.newThreadVisible = false;
            switch($scope.currentCommentsScope) {
                case '0':
                    $scope.comments = [];
                    $scope.newThreadVisible = true;
                    $scope.currentThread = {'name': ''};
                    $scope.commentsHeaderText = 'Nowy wątek: ';
                    $scope.currentThreadOptionDisabled = true;
                    $scope.userViewIconVisible = false;
                    $scope.searchCommentsIconVisible = false;
                    $scope.searchCommentsVisible = false;
                    $scope.statisticsIconVisible = false;
                    $scope.newThreadOptionDisabled = false;
                    $scope.statisticsVisible = false;
                    $window.document.getElementById('searchTextArea').style.right = "125px";
                    break;
                case '1':
                    $scope.comments = [];
                    $scope.commentsHeaderText = 'Tytuł wątku:';
                    $scope.userViewIconVisible = true;
                    $scope.searchCommentsIconVisible = true;
                    $scope.statisticsIconVisible = true;
                    $scope.currentThreadOptionDisabled = false;
                    $scope.commentsUserFilter = {};
                    $scope.sortedByUser = 'NTP (globalna spójność)';
                    $scope.currentUserScope = '';
                    $scope.currentThread.unreadcomment = false;             // TODO WHAT TO DO?
                                                                            // TODO MARKING THREAD AS READ
                    $window.document.getElementById('searchTextArea').style.right = "125px";
                    $window.setTimeout(function() { $scope.scrollDown(); }, 60);
                    break;
                case '2':
                    $scope.comments = [];
                    getCommentsFromPath();
                    $scope.commentsHeaderText = 'Wszystkie komentarze z lokalizacji';
                    $scope.currentThread = {'name': ''};
                    $scope.userViewIconVisible = false;
                    $scope.searchCommentsIconVisible = true;
                    $scope.statisticsIconVisible = true;
                    $scope.commentsUserFilter = {};
                    $scope.newThreadVisible = false;
                    $window.document.getElementById('searchTextArea').style.right = "98px";
                    break;
                case '3':
                    $scope.commentsHeaderText = 'Wszystkie komentarze użytkownika:';
                    $scope.currentThread = {'name': ''};
                    $scope.userViewIconVisible = false;
                    $scope.searchCommentsIconVisible = true;
                    $scope.statisticsIconVisible = false;
                    $scope.globalCommentsMenuVisible = true;
                    if ($scope.statisticsVisible)
                        $scope.showStatisticsButtonClicked();
                    $window.document.getElementById('searchTextArea').style.right = "70px";
                    break;
                default:
                    $scope.comments = [];
                    $scope.commentsHeaderText = 'Wszystkie komentarze z folderu';
                    $scope.currentThread = {'name': ''};
                    $scope.userViewIconVisible = false;
                    $scope.searchCommentsIconVisible = true;
                    $scope.statisticsIconVisible = true;
                    getAllComments();
                    $window.document.getElementById('searchTextArea').style.right = "98px";
                    break;
            }
        };
        $scope.changeUserScope = function() {
            getAllComments();
            $scope.commentsUserFilter.uid = $scope.currentUserScope;
            makeAllInvisible();
        };
        $scope.sortByUser = function() {
            makeAllInvisible();
            if ($scope.sortedByUser != 'NTP (globalna spójność)')   refreshComments($scope.sortedByUser);
        };
        $scope.showStatisticsButtonClicked = function() { $scope.statisticsVisible = !$scope.statisticsVisible; };

        // #######################################  COMMENTS BODY  ####################################################

        // ARRAYS, DICTIONARIES, ETC.
        $scope.comments = [];
        $scope.stats = {};
        $scope.commentsUserFilter = {};
        $scope.UIDtoIdentity = {};
        refreshComments = function(uid) {
            $http.post('/getcomments', {'fullthreadpath': $scope.currentThread.fullpath,
                                        'sortinguid': uid}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    $scope.comments = response.data.comments;
                    $scope.stats = response.data.stats;
                }
            );
        };

        // STRINGS
        $scope.newCommentText = '';
        $scope.newThreadPath = '';
        $scope.newThreadFile = '';
        $scope.newThreadComment = '';

        // BOOLEANS
        $scope.newThreadVisible = false;
        $scope.newThreadLocalizationInputVisible = false;
        $scope.newThreadFileInputVisible = false;

        $scope.scrollDown = function() { $window.jQuery("#commentsView").mCustomScrollbar("scrollTo", "bottom"); };
        $scope.writeComment = function() {
            if ($scope.newCommentText.length === 0) {
                $.notify('Wpisz treść komentarza.', {className: 'error', position: 'right bottom'});
                return;
            }

            $http.post('/writecomment', {'comment': $scope.newCommentText,
                                         'fullthreadpath': $scope.currentThread.fullpath}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    // REFRESH COMMENTS AND SCROLLDOWN
                    refreshComments('');
                    $window.setTimeout(function() { $scope.scrollDown(); }, 60);
                    $scope.newCommentText = '';
                    $scope.currentThread.numberofcomments += 1;
                    $scope.currentThread.lastcomment = response.data.timestamp;
                }
            )
        };
        $scope.writeNewThread = function() {
            if ($scope.currentThread.name.length === 0 || $scope.newThreadComment.length === 0) {
                $.notify('Tytuł oraz treść nie mogą być puste.', {className: 'error', position: 'right bottom'});
                return;
            }
            if ($scope.currentThread.name.indexOf("@#&$") > -1) {
                $.notify('Ciąg znaków @#&$ nie może być zawarty w tytule wątku.', {className: 'error', position: 'right bottom'});
                return;
            }
            if ($scope.currentThread.name.indexOf(".") > -1) {
                $.notify('Znak "." nie może występować w tytule wątku.', {className: 'error', position: 'right bottom'});
                return;
            }

            $http.post('/writenewthread', {'topic': $scope.currentThread.name,
                                           'comment': $scope.newThreadComment,
                                           'insidepath': $scope.newThreadPath,
                                           'folderpath': $scope.currentFolder.dir,
                                           'fileabout': $scope.newThreadFile}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                }
            ).then(
                function(response) {
                    $scope.currentThread = response.data;
                    $scope.threadClicked($scope.currentThread);
                    refreshFiles();
                }
            )
        };
        $scope.editComment = function(comment) {
            comment.editing = false;
            $http.post('/editcomment', {'comment': comment, 'threadfullpath': $scope.currentThread.fullpath}).error(
                function(response) {
                    $.notify(response, {className: 'error', position: 'right bottom'});
                    refreshComments('');
                }
            ).then(
                function (response) {
                    //$window.alert('powodzenie');
                    //$scope.files = response.data;
                    refreshComments('');
                }
            );
        };
    }]);
})();