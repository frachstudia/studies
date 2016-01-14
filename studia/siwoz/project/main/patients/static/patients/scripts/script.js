(function(){
    //'use strict';

    var app = angular.module('patientsApp', ['ngResource']);

    app.config(
        //['$httpProvider', function($httpProvider, $cookies) { $httpProvider.defaults.headers.common['X-CSRFToken'] = $cookies['csrftoken']; }],
        ['$httpProvider', function($httpProvider) {
            $httpProvider.defaults.xsrfCookieName = 'csrftoken';
            $httpProvider.defaults.xsrfHeaderName = 'X-CSRFToken';
        }],
        ['$routeProvider', function($routeProvider) { $routeProvider.when('/[0-9]{11}', {templateUrl: 'patients/templates/patients/mainSite.html'}) }],
        ['$interpolateProvider'], function($interpolateProvider) { $interpolateProvider.startSymbol('{$'); $interpolateProvider.endSymbol('$}'); }
    );

    // VISIT FACTORY
    app.factory('Visit', function($resource) {
        return $resource('/patients/visits/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // RESULT FACTORY
    app.factory('Result', function($resource) {
        return $resource('/patients/results/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // CATEGORY FACTORY
    app.factory('Category', function($resource) {
        return $resource('/patients/categorys/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // TESTCATEGORY FACTORY
    app.factory('TestCategory', function($resource) {
        return $resource('/patients/testcategorys/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // UNITS FACTORY
    app.factory('Unit', function($resource) {
        return $resource('/patients/units/:id', {id: '@id'});
    });

    // PLACES FACTORY
    app.factory('Place', function($resource) {
        return $resource('/patients/places/:id', {id: '@id'});
    });

    // DOCTORS FACTORY
    app.factory('Doctor', function($resource) {
        return $resource('/patients/doctors/:id', {id: '@id'});
    });

    // TESTS FACTORY
    app.factory('Test', function($resource) {
        return $resource('/patients/tests/:id', {id: '@id'});
    });

    // PERIODIC TESTS FACTORY
    app.factory('PeriodicTest', function($resource) {
        return $resource('/patients/periodictests/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // PROFILE RESULTS FACTORY
    app.factory('ProfileResult', function($resource) {
        return $resource('/patients/profileresults/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // PROFILES FACTORY
    app.factory('Profile', function($resource) {
        return $resource('/patients/profiles/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // TESTPROFILE FACTORY
    app.factory('TestProfile', function($resource) {
        return $resource('/patients/testprofiles/:id', {id: '@id'}, {update: {method: 'PUT'}});
    });

    // CONTROLLER FOR LOGIN SITE
    app.controller('PanelController', function() {
        this.tab = 1;

        this.selectTab = function(setTab) {
            this.tab = setTab;
        };

        this.isSelected = function(checkTab) {
            return this.tab === checkTab;
        };
    });

    // CONTROLLER FOR MAIN NAV MENU + LOGOUT ETC.
    app.controller('MainMenuController', function($window, $scope) {
        this.tab = 1;
        this.optionsTab = false;

        this.selectTab = function(setTab) {
            this.tab = setTab;
            this.optionsTab = false;
        };

        this.isSelected = function(checkTab) {
            return this.tab === checkTab;
        };

        this.selectOptionsTab = function() {
            this.optionsTab = true;
        };

        this.changePassword = function() {
            $window.$.notify('Tu będzie zmiana hasła', {'className': 'info', 'globalPosition': 'bottom right'});
            // todo
        };

        this.logout = function() {
            $window.$.notify('Tu będzie wylogowanie', {'className': 'info', 'globalPosition': 'bottom right'});
            $window.location.assign('http://localhost:8000/patients/logout');
        };

        this.deleteAccount = function() {
            $window.$.notify('Tu będzie usunięcie konta', {'className': 'info', 'globalPosition': 'bottom right'});
            // todo
        };
    });

    // CONTROLLER FOR 'TESTS' SUBMENU
    app.controller('TestsController', function() {
        this.tab = 1;

        this.selectTab = function(setTab) {
            this.tab = setTab;
        };

        this.isSelected = function(checkTab) {
            return this.tab === checkTab;
        };
    });

    // CONTROLLER FOR 'VISITS' SUBMENU
    app.controller('VisitsController', function() {
        this.tab = 1;

        this.selectTab = function(setTab) {
            this.tab = setTab;
        };

        this.isSelected = function(checkTab) {
            return this.tab === checkTab;
        };
    });

    // CONTROLLER FOR 'REPORTS' SUBMENU
    app.controller('ReportsController', ['$scope', '$window', 'Category', 'Place', 'Test',
    function($scope, $window, Category, Place, Test) {
        this.tab = 1;

        this.selectTab = function(setTab) {
            this.tab = setTab;
        };

        this.isSelected = function(checkTab) {
            return this.tab === checkTab;
        };

        // DATA CONTROLLER
        $scope.tests = Test.query();
        $scope.selectedTests = {};
        $scope.places = Place.query();
        $scope.selectedPlace = {};
        $scope.dateLabelText = 'Cały przedział czasowy';
        $scope.startDate = '';
        $scope.stopDate = '';

        $scope.changeDate = function() {
            if ($scope.stopDate !== '' && $scope.startDate > $scope.stopDate) {
                $window.$.notify('Data "Od" nie może być późniejsza od daty "Do".');
                $scope.startDate = '';
                $scope.stopDate = '';
            }

            if ($scope.startDate === '' && $scope.stopDate === '')
                $scope.dateLabelText = 'Cały przedział czasowy';
            else
                if ($scope.startDate === '')
                    $scope.dateLabelText = 'Do ' + $scope.stopDate;
                else
                    if ($scope.stopDate === '')
                        $scope.dateLabelText = 'Od ' + $scope.startDate;
                    else
                        $scope.dateLabelText = 'Od ' + $scope.startDate + ' do ' + $scope.stopDate;
        };
    }]);

    app.controller('visitsDataController', ['$scope', 'Visit', function($scope, Visit) {
        $scope.visits = Visit.query();
    }]);

    app.controller('allDataController', ['$scope', '$window', '$filter', 'Category', 'Test', 'TestCategory', 'Place', 'PeriodicTest', 'Doctor', 'Result', 'Visit',
    function($scope, $window, $filter, Category, Test, TestCategory, Place, PeriodicTest, Doctor, Result, Visit) {
        $scope.categorys = Category.query();
        $scope.tests = Test.query();
        $scope.testcategorys = TestCategory.query();
        $scope.places = Place.query();
        $scope.periodictests = PeriodicTest.query();
        $scope.doctors = Doctor.query();
        $scope.result = Result.query();
        $scope.visits = Visit.query();

        // CATEGORYS DATA CONTROLLER
        $scope.newCategory = new Category();
        $scope.showEdit = false;
        $scope.showNew = false;
        $scope.newCategoryButtonText = 'Dodaj nową kategorię';
        $scope.testInCategory = {};
        $scope.selectCategory = function(cat) {
            $scope.showEdit = true;
            $scope.activeCategory = cat;

            // EVERYTHING SET ON FALSE
            for (var i = 0; i < $scope.tests.length; i++) {
                $scope.testInCategory[$scope.tests[i].id] = { value: false, name: $scope.tests[i].name};
            }

            // SET ON TRUE IF IN DATABASE
            for (var i = 0; i < $scope.testcategorys.length; i++) {
                if ($scope.testcategorys[i].category == $scope.activeCategory.id)
                    $scope.testInCategory[$scope.testcategorys[i].test].value = true;
            }

            $scope.testcategorys = TestCategory.query();
        };
        $scope.changeState = function(id) {
            // FUNCTION AFTER(!!!) CHANGING STATE
            var errorless = true;

            if ($scope.testInCategory[id].value === false) {
                for (var i = 0; i < $scope.testcategorys.length; i++) {
                    if ($scope.testcategorys[i].category === $scope.activeCategory.id && $scope.testcategorys[i].test === id) {
                        $scope.testcategorys[i].$delete(function() {}, function() {errorless = false;});
                        break;
                    }
                }

                if (errorless) {
                    $window.$.notify('Usunięto badanie ' + $scope.testInCategory[id].name + ' z kategorii ' + $scope.activeCategory.name, 'success');
                }
                else
                    $window.$.notify('Nie udało się usunąć badania ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
            } else {
                var newTestCategory = new TestCategory();
                newTestCategory.test = id;
                newTestCategory.category = $scope.activeCategory.id;
                newTestCategory.$save(function() {}, function () {errorless = false;});

                if (errorless) {
                    $window.$.notify('Dodano badanie ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
                }
                else
                    $window.$.notify('Nie udało się dodać badania ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
            }

            $scope.testcategorys = TestCategory.query();
        }
        $scope.clearSelection = function() {
            $scope.showEdit = false;
            $scope.activeCategory = null;
        };
        $scope.addCategoryClick = function() {
            if ($scope.showNew === true) {
                $scope.showNew = false;
                $scope.newCategoryButtonText = 'Dodaj nową kategorię';
            } else {
                $scope.showNew = true;
                $scope.newCategoryButtonText = 'Ukryj dodawanie';
            }
        };
        $scope.addCat = function() {
            var errorless = true;

            $scope.newCategory.$save(function() {}, function() {errorless = false;});

            if (errorless) {
                var newCatName = $scope.newCategory.name;
                // ADD TO ARRAY
                $scope.categorys.push($scope.newCategory);

                // SET NEW ACTIVE DIRECTORY AND PREPARE LAYOUT
                $scope.selectCategory($scope.newCategory);
                $scope.showEdit = true;
                $scope.showNew = false;
                $scope.newCategoryButtonText = 'Dodaj nową kategorię';

                $scope.newCategory = new Category();
                $window.$.notify('Dodano kategorię ' + newCatName, 'success');
            } else {
                $window.$.notify('Nie udało się dodać kategorii ' + newCatName, 'error');
            }
        };
        $scope.deleteCat = function() {
            var errorless = true;
            $scope.activeCategory.$delete(function() {}, function() {errorless = false;});

            if (errorless) {
                // REMOVE FROM ARRAY
                var nameOfCat = $scope.activeCategory.name;
                var index = $scope.categorys.indexOf($scope.activeCategory);
                $scope.categorys.splice(index, 1);

                // PREPARE LAYOUT
                $scope.showEdit = false;
                $scope.activeCategory = null;
                $window.$.notify('Usunięto kategorię ' + nameOfCat, 'success');
                $scope.showEdit = false;
            } else {
                $window.$.notify('Nie udało się usunąć kategorii ' + nameOfCat, 'error');
            }
        };
        $scope.changeCat = function() {
            var errorless = true;

            $scope.activeCategory.$update(function() {}, function() {errorless = false;});

            if (errorless) {
                // PREPARE LAYOUT
                $window.$.notify('Zmieniono nazwę na ' + $scope.activeCategory.name, 'success');
            } else {
                $window.$.notify('Nie udało się zmienić nazwy.', 'error');
            }
        };
        // END CATEGORYS DATA CONTROLLER

        // RESULTS DATA CONTROLLER
        $scope.selectedResult = {};
        $scope.selectedTest = {};
        $scope.selectedPlace = {};
        $scope.result1 = '';
        $scope.result2 = '';

        $scope.newResult = new Result();
        $scope.newResult.date = $filter('date')(Date.now(), 'yyyy-MM-dd');
        $scope.newResult.resulttype = 'numeric';

        $scope.labelText = 'Wynik ilościowy:';
        $scope.numericResult = true;
        $scope.editDialogVisible = false;
        $scope.deleteDialogVisible = false;

        $scope.switchResult = function() {
            if ($scope.numericResult) {
                $scope.labelText = 'Wynik opisowy:';
                $scope.numericResult = false;
                $scope.newResult.resulttype = 'description';
                $scope.result1 = '';
            } else {
                $scope.labelText = 'Wynik ilościowy:';
                $scope.numericResult = true;
                $scope.newResult.resulttype = 'numeric';
                $scope.result2 = '';
            }
        };
        $scope.postResult = function() {
            $scope.newResult.test = $scope.selectedTest.id;
            $scope.newResult.place = $scope.selectedPlace.id;

            if ($scope.numericResult) {
                $scope.newResult.result = $scope.result1;
            } else {
                $scope.newResult.result = $scope.result2;
            }

            $scope.newResult.$save(function() {
                // CLEAR FIELDS
                $scope.selectedTest = {}
                $scope.selectedPlace = {}
                $scope.newResult = new Result();
                $scope.newResult.date = $filter('date')(Date.now(), 'yyyy-MM-dd');
                $scope.newResult.result = '';
                $scope.result1 = '';
                $scope.result2 = '';
                $scope.newResult.resulttype = 'numeric';
                $scope.newResult.comment = '';

                $scope.results.push($scope.newResult);
                $scope.numericResult = true;

                $window.$.notify('Dodano wynik', 'success');
            }).catch(function () {
                $window.$.notify('Pola "Badanie" oraz "Wynik" muszą być wypełnione.', 'error');
                return;
            });
        };
        $scope.selectTest = function(test) {
            $scope.selectedTest = test;
        };

        $scope.selectPlace = function(place) {
            $scope.selectedPlace = place;
        };
        $scope.showEditDialog = function(id) {
            // GET CLICKED RESULT
            for (var i = 0; i < $scope.results.length; i++) {
                if ($scope.results[i].id === id) {
                    $scope.selectedResult = $scope.results[i];
                    break;
                }
            }

            // GET RESULT'S PLACE
            for (var i = 0; i < $scope.places.length; i++) {
                if ($scope.places[i].id === $scope.selectedResult.place) {
                    $scope.selectedPlace = $scope.places[i];
                    break;
                }
            }

            // GET RESULT'S TEST
            for (var i = 0; i < $scope.tests.length; i++) {
                if ($scope.tests[i].id === $scope.selectedResult.test) {
                    $scope.selectedTest = $scope.tests[i];
                    break;
                }
            }

            $scope.selectedTest.comment = 'DUPA';
                $window.alert($scope.selectedPlace.name);
                $window.alert($scope.selectedTest.name);

            $scope.editDialogVisible = true;
        };
        $scope.closeEditDialog = function() {
            $scope.editDialogVisible = false;
        };
        $scope.showDeleteDialog = function(id) {
            $scope.selectedResult = Result.get({id: id});

            $scope.deleteDialogVisible = true;
        };
        $scope.closeDeleteDialog = function() {
            $scope.deleteDialogVisible = false;
        };
        $scope.editResult = function(id) {
            $scope.selectedResult = Result.get({id: id});

            $window.$.notify('Tu będzie edycja tego oto badania', 'info');
        };
        $scope.deleteResult = function(id) {
            var errorless = true;

            $scope.visits.splice( $.inArray($scope.editedVisit, $scope.visits), 1 );

            $scope.selectedResult.$delete(function() {}, function() {errorless = false;});

            if (errorless) {
                $scope.deleteDialogVisible = false;
                $window.$.notify('Usunięto wynik.', 'success');
            } else {
                $window.$.notify('Nie udało się usunąć wyniku.', 'error');
            }
        };
        // END RESULTS DATA CONTROLLER

        // VISITS DATA CONTROLLER
        $scope.newVisit = new Visit();
        $scope.newVisitDate = '';
        $scope.newVisitTime = '';

        $scope.selectedPlace = {};
        $scope.selectedDoctor = {};
        $scope.editedVisit = {};
        $scope.editVisitVisible = false;
        $scope.deleteVisitVisible = false;

        $scope.postVisit = function() {
            $scope.newVisit.date = $scope.newVisitDate.concat('T', $scope.newVisitTime)

            $scope.newVisit.place = $scope.selectedPlace.id;
            $scope.newVisit.doctor = $scope.selectedDoctor.id;

            $scope.newVisit.$save(function() {
                // CLEAR FIELDS
                $scope.selectedDoctor = {}
                $scope.selectedPlace = {}
                $scope.newVisit = new Visit();
                $scope.newVisitDate = '';
                $scope.newVisitTime = '';
                $scope.visits = Visit.query();

                $window.$.notify('Dodano wizytę', 'success');
            }).catch(function () {
                $window.$.notify('Nie udało się dodać wizyty. Upewnij się, że wszystkie pola są poprawne.', 'error');
                return;
            });
        };
        $scope.isPast = function(date) {
            if (new Date(date) < Date.now())
                return true

            return false;
        };
        $scope.showEditVisitDialog = function(id) {
            // GET VISIT
            for (var i = 0; i < $scope.visits.length; i++) {
                if ($scope.visits[i].id === id) {
                    $scope.editedVisit = $scope.visits[i];
                    break;
                }
            }

            for (var i = 0; i < $scope.doctors.length; i++) {
                if ($scope.doctors[i].id === $scope.editedVisit.doctor) {
                    $scope.selectedDoctor = $scope.doctors[i];
                    break;
                }
            }

            for (var i = 0; i < $scope.places.length; i++)
                if ($scope.places[i].id === $scope.editedVisit.place) {
                    $scope.selectedPlace = $scope.places[i];
                    break;
                }

            $scope.editedDate = $scope.editedVisit.date.split('T')[0];
            $scope.editedTime = $scope.editedVisit.date.split('T')[1].substring(0, 5);

            $scope.editVisitVisible = true;
        };
        $scope.closeEditVisitDialog = function() {
            $scope.editVisitVisible = false;
            $scope.editedVisit = {};
            $scope.selectedPlace = {};
            $scope.selectedDoctor = {};
        };
        $scope.saveVisitChanges = function() {
            var errorless = true;

            $scope.editedVisit.date = $scope.editedDate.concat('T', $scope.editedTime);
            $scope.editedVisit.place = $scope.selectedPlace.id;
            $scope.editedVisit.doctor = $scope.selectedDoctor.id;

            $scope.editedVisit.$update(function() {}, function() {errorless = false;});

            if (errorless) {
                $window.$.notify('Zapisano zmiany w wizycie o nazwie ' + $scope.editedVisit.name, 'success');
            } else {
                $window.$.notify('Nie udało się zapisać zmian w wizycie.', 'error');
            }

            $scope.editVisitVisible = false;
            $scope.editedVisit = {};
            $scope.selectedPlace = {};
            $scope.selectedDoctor = {};
        };
        $scope.showDeleteVisitDialog = function() {
            $scope.deleteVisitVisible = true;
        };
        $scope.closeDeleteVisitDialog = function() {
            $scope.deleteVisitVisible = false;
        };
        $scope.deleteVisit = function() {
            var errorless = true;

            $scope.editedVisit.$delete(function() {}, function() {errorless = false;});

            if (errorless) {
                $scope.visits.splice( $.inArray($scope.editedVisit, $scope.visits), 1 );

                $scope.editedVisit = {};
                $scope.selectedPlace = {};
                $scope.selectedDoctor = {};

                $scope.deleteVisitVisible = false;
                $scope.editVisitVisible = false;
                $window.$.notify('Usunięto wizytę.', 'success');
            } else {
                $window.$.notify('Nie udało się usunąć wizyty.', 'error');
            }
        };
        // END VISITS DATA CONTROLLER

        // PERIODICTESTS DATA CONTROLLER

        // END PERIODICTESTS DATA CONTROLLER
    }]);

    app.controller('categorysDataController', ['$scope', '$window', 'Category', 'Test', 'TestCategory',
    function($scope, $window, Category, Test, TestCategory) {
        $scope.categorys = Category.query();
        $scope.newCategory = new Category();
        $scope.tests = Test.query();
        $scope.testcategorys = TestCategory.query();
        this.showEdit = false;
        this.showNew = false;
        this.newCategoryButtonText = 'Dodaj nową kategorię';

        $scope.testInCategory = {};

        this.selectCategory = function(cat) {
            this.showEdit = true;
            $scope.activeCategory = cat;

            // EVERYTHING SET ON FALSE
            for (var i = 0; i < $scope.tests.length; i++) {
                $scope.testInCategory[$scope.tests[i].id] = { value: false, name: $scope.tests[i].name};
            }

            // SET ON TRUE IF IN DATABASE
            for (var i = 0; i < $scope.testcategorys.length; i++) {
                if ($scope.testcategorys[i].category == $scope.activeCategory.id)
                    $scope.testInCategory[$scope.testcategorys[i].test].value = true;
            }

            $scope.testcategorys = TestCategory.query();
        };

        $scope.changeState = function(id) {
            // FUNCTION AFTER(!!!) CHANGING STATE
            var errorless = true;

            if ($scope.testInCategory[id].value === false) {
                for (var i = 0; i < $scope.testcategorys.length; i++) {
                    if ($scope.testcategorys[i].category === $scope.activeCategory.id && $scope.testcategorys[i].test === id) {
                        $scope.testcategorys[i].$delete(function() {}, function() {errorless = false;});
                        break;
                    }
                }

                if (errorless) {
                    $window.$.notify('Usunięto badanie ' + $scope.testInCategory[id].name + ' z kategorii ' + $scope.activeCategory.name, 'success');
                }
                else
                    $window.$.notify('Nie udało się usunąć badania ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
            } else {
                var newTestCategory = new TestCategory();
                newTestCategory.test = id;
                newTestCategory.category = $scope.activeCategory.id;
                newTestCategory.$save(function() {}, function () {errorless = false;});

                if (errorless) {
                    $window.$.notify('Dodano badanie ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
                }
                else
                    $window.$.notify('Nie udało się dodać badania ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeCategory.name, 'success');
            }

            $scope.testcategorys = TestCategory.query();
        }

        this.clearSelection = function() {
            this.showEdit = false;
            $scope.activeCategory = null;
        };

        $scope.compareFn = function(obj1, obj2){
            return obj1.id === obj2.id;
        };

        this.addCategoryClick = function() {
            if (this.showNew === true) {
                this.showNew = false;
                this.newCategoryButtonText = 'Dodaj nową kategorię';
            } else {
                this.showNew = true;
                this.newCategoryButtonText = 'Ukryj dodawanie';
            }
        };

        this.addCat = function() {
            var errorless = true;

            $scope.newCategory.$save(function() {}, function() {errorless = false;});

            if (errorless) {
                var newCatName = $scope.newCategory.name;
                // ADD TO ARRAY
                $scope.categorys.push($scope.newCategory);

                // SET NEW ACTIVE DIRECTORY AND PREPARE LAYOUT
                this.selectCategory($scope.newCategory);
                $scope.tests = Test.query();
                this.showEdit = true;
                this.showNew = false;
                this.newCategoryButtonText = 'Dodaj nową kategorię';

                $scope.newCategory = new Category();
                $window.$.notify('Dodano kategorię ' + newCatName, 'success');
            } else {
                $window.$.notify('Nie udało się dodać kategorii ' + newCatName, 'error');
            }
        };

        this.deleteCat = function() {
            var errorless = true;
            $scope.activeCategory.$delete(function() {}, function() {errorless = false;});

            if (errorless) {
                // REMOVE FROM ARRAY
                var nameOfCat = $scope.activeCategory.name;
                var index = $scope.categorys.indexOf($scope.activeCategory);
                $scope.categorys.splice(index, 1);

                // PREPARE LAYOUT
                this.showEdit = false;
                $scope.activeCategory = null;
                $window.$.notify('Usunięto kategorię ' + nameOfCat, 'success');
                this.showEdit = false;
            } else {
                $window.$.notify('Nie udało się usunąć kategorii ' + nameOfCat, 'error');
            }
        };

        this.changeCat = function() {
            var errorless = true;

            $scope.activeCategory.$update(function() {}, function() {errorless = false;});

            if (errorless) {
                // PREPARE LAYOUT
                $window.$.notify('Zmieniono nazwę na ' + $scope.activeCategory.name, 'success');
            } else {
                $window.$.notify('Nie udało się zmienić nazwy.', 'error');
            }
        };
    }]);

    app.controller('resultsDataController', ['$scope', '$filter', '$window', '$q', 'Result', 'Place', 'Test',
    function($scope, $filter, $window, $q, Result, Place, Test) {
        $scope.results = Result.query();
        $scope.places = Place.query();
        $scope.tests = Test.query();
        $scope.selectedResult = {};
        $scope.selectedTest = {};
        $scope.selectedPlace = {};
        $scope.result1 = '';
        $scope.result2 = '';
        $scope.orderBy = 'date';
        $scope.editedTest = {};
        $scope.editedPlace = {};

        $scope.newResult = new Result();
        $scope.newResult.date = $filter('date')(Date.now(), 'yyyy-MM-dd');
        $scope.newResult.result = '';
        $scope.newResult.resulttype = 'numeric';
        $scope.newResult.comment = '';

        $scope.labelText = 'Wynik ilościowy:';
        $scope.numericResult = true;
        $scope.editDialogVisible = false;
        $scope.deleteDialogVisible = false;

        $scope.switchResult = function() {
            if ($scope.numericResult) {
                $scope.labelText = 'Wynik opisowy:';
                $scope.numericResult = false;
                $scope.newResult.resulttype = 'description';
                $scope.result1 = '';
            } else {
                $scope.labelText = 'Wynik ilościowy:';
                $scope.numericResult = true;
                $scope.newResult.resulttype = 'numeric';
                $scope.result2 = '';
            }
        };

        $scope.postResult = function() {
            $scope.newResult.test = $scope.selectedTest.id;
            $scope.newResult.place = $scope.selectedPlace.id;

            if ($scope.numericResult) {
                $scope.newResult.result = $scope.result1;
            } else {
                $scope.newResult.result = $scope.result2;
            }

            $scope.newResult.$save(function() {
                // CLEAR FIELDS
                $scope.selectedTest = {}
                $scope.selectedPlace = {}
                $scope.newResult = new Result();
                $scope.newResult.date = $filter('date')(Date.now(), 'yyyy-MM-dd');
                $scope.newResult.result = '';
                $scope.result1 = '';
                $scope.result2 = '';
                $scope.newResult.resulttype = 'numeric';
                $scope.newResult.comment = '';

                $scope.results.push($scope.newResult);
                $scope.numericResult = true;

                $window.location.assign('/');

                $window.$.notify('Dodano wynik', 'success');
            }).catch(function () {
                $window.$.notify('Pola "Badanie" oraz "Wynik" muszą być wypełnione.', 'error');
                return;
            });
        };

        $scope.selectTest = function(test) {
            $scope.selectedTest = test;
            $scope.editedTest = test;
        };

        $scope.selectPlace = function(place) {
            $scope.selectedPlace = place;
            $scope.editedPlace = place;
        };

        $scope.showEditDialog = function(id) {
            // GET CLICKED RESULT
            for (var i = 0; i < $scope.results.length; i++) {
                if ($scope.results[i].id === id) {
                    $scope.selectedResult = $scope.results[i];
                    break;
                }
            }

            // GET RESULT'S PLACE
            for (var i = 0; i < $scope.places.length; i++) {
                if ($scope.places[i].id === $scope.selectedResult.place) {
                    $scope.selectedPlace = $scope.places[i];
                    break;
                }
            }

            // GET RESULT'S TEST
            for (var i = 0; i < $scope.tests.length; i++) {
                if ($scope.tests[i].id === $scope.selectedResult.test) {
                    $scope.selectedTest = $scope.tests[i];
                    break;
                }
            }

            $scope.editDialogVisible = true;
        };

        $scope.closeEditDialog = function() {
            $scope.selectedResult = {};
            $scope.selectedTest = {};
            $scope.selectedPlace = {};

            $scope.editDialogVisible = false;
        };

        $scope.showDeleteDialog = function() {
            $scope.deleteDialogVisible = true;
        };

        $scope.closeDeleteDialog = function() {
            $scope.deleteDialogVisible = false;
        };

        $scope.commitEdit = function() {
            $scope.selectedResult.$update(function(){
                $window.$.notify('Zapisano zmiany.', 'success');

                $window.location.assign('/');
                //$scope.closeEditDialog();     NO NEED TO DO THAT
            }, function() {
                $window.$.notify('Nie udało się zapisać.', 'error');
            });
        };

        $scope.deleteResult = function() {
            $scope.selectedResult.$delete(function() {
                $window.location.assign('/');
                //$scope.closeDeleteDialog();
                //$scope.closeEditDialog();
                //$window.$.notify('Usunięto wynik.', 'success');       NO NEED ...
            }, function() {
                $window.$.notify('Nie udało się usunąć wyniku.', 'error');
            });
        };
    }]);

    app.controller('visitsDataController', ['$scope', '$filter', '$window', '$q', 'Visit', 'Place', 'Doctor',
    function($scope, $filter, $window, $q, Visit, Place, Doctor) {
        $scope.visits = Visit.query();
        $scope.places = Place.query();
        $scope.doctors = Doctor.query();
        $scope.newVisit = new Visit();
        $scope.newVisitDate = '';
        $scope.newVisitTime = '';

        $scope.selectedPlace = {};
        $scope.selectedDoctor = {};
        $scope.editedVisit = {};
        $scope.editVisitVisible = false;
        $scope.deleteVisitVisible = false;

        $scope.postVisit = function() {
            $scope.newVisit.date = $scope.newVisitDate.concat('T', $scope.newVisitTime)

            $scope.newVisit.place = $scope.selectedPlace.id;
            $scope.newVisit.doctor = $scope.selectedDoctor.id;

            $scope.newVisit.$save(function() {
                // CLEAR FIELDS
                $scope.selectedDoctor = {}
                $scope.selectedPlace = {}
                $scope.newVisit = new Visit();
                $scope.newVisitDate = '';
                $scope.newVisitTime = '';
                $scope.visits = Visit.query();

                $window.$.notify('Dodano wizytę', 'success');
            }).catch(function () {
                $window.$.notify('Nie udało się dodać wizyty. Upewnij się, że wszystkie pola są poprawne.', 'error');
                return;
            });
        };

        $scope.isPast = function(date) {
            if (new Date(date) < Date.now())
                return true

            return false;
        };

        $scope.showEditVisitDialog = function(id) {
            // GET VISIT
            for (var i = 0; i < $scope.visits.length; i++) {
                if ($scope.visits[i].id === id) {
                    $scope.editedVisit = $scope.visits[i];
                    break;
                }
            }

            for (var i = 0; i < $scope.doctors.length; i++) {
                if ($scope.doctors[i].id === $scope.editedVisit.doctor) {
                    $scope.selectedDoctor = $scope.doctors[i];
                    break;
                }
            }

            for (var i = 0; i < $scope.places.length; i++)
                if ($scope.places[i].id === $scope.editedVisit.place) {
                    $scope.selectedPlace = $scope.places[i];
                    break;
                }

            $scope.editedDate = $scope.editedVisit.date.split('T')[0];
            $scope.editedTime = $scope.editedVisit.date.split('T')[1].substring(0, 5);

            $scope.editVisitVisible = true;
        };

        $scope.closeEditVisitDialog = function() {
            $scope.editVisitVisible = false;
            $scope.editedVisit = {};
            $scope.selectedPlace = {};
            $scope.selectedDoctor = {};
        };

        $scope.saveVisitChanges = function() {
            var errorless = true;

            $scope.editedVisit.date = $scope.editedDate.concat('T', $scope.editedTime);
            $scope.editedVisit.place = $scope.selectedPlace.id;
            $scope.editedVisit.doctor = $scope.selectedDoctor.id;

            $scope.editedVisit.$update(function() {}, function() {errorless = false;});

            if (errorless) {
                $window.$.notify('Zapisano zmiany w wizycie o nazwie ' + $scope.editedVisit.name, 'success');
            } else {
                $window.$.notify('Nie udało się zapisać zmian w wizycie.', 'error');
            }

            $scope.editVisitVisible = false;
            $scope.editedVisit = {};
            $scope.selectedPlace = {};
            $scope.selectedDoctor = {};
        };

        $scope.showDeleteVisitDialog = function() {
            $scope.deleteVisitVisible = true;
        };

        $scope.closeDeleteVisitDialog = function() {
            $scope.deleteVisitVisible = false;
        };

        $scope.deleteVisit = function() {
            var errorless = true;

            $scope.editedVisit.$delete(function() {}, function() {errorless = false;});

            if (errorless) {
                $scope.visits.splice( $.inArray($scope.editedVisit, $scope.visits), 1 );

                $scope.editedVisit = {};
                $scope.selectedPlace = {};
                $scope.selectedDoctor = {};

                $scope.deleteVisitVisible = false;
                $scope.editVisitVisible = false;
                $window.$.notify('Usunięto wizytę.', 'success');
            } else {
                $window.$.notify('Nie udało się usunąć wizyty.', 'error');
            }
        };
    }]);

    app.controller('periodicTestsDataController', ['$scope', '$filter', '$window', 'Place', 'Test', 'PeriodicTest', 'Doctor',
    function($scope, $filter, $window, Place, Test, PeriodicTest, Doctor) {
        $scope.periodictests = PeriodicTest.query();
        $scope.doctors = Doctor.query();
        $scope.places = Place.query();
        $scope.tests = Test.query();
        $scope.selectedTest = {};
        $scope.selectedPlace = {};
        $scope.selectedDoctor = {};

        $scope.intervalTypeOptions = [
            {'unit': 'dni', 'value': 'DAY'},
            {'unit': 'tygodnie', 'value': 'WEEK'},
            {'unit': 'miesiące', 'value': 'MONTH'},
            {'unit': 'lata', 'value': 'YEAR'},
        ];

        $scope.newPeriodicTest = new PeriodicTest();
        $scope.newPeriodicTest.startdate = $filter('date')(Date.now(), 'yyyy-MM-dd');
    }]);

    app.controller('shareResultsController', ['$scope', '$window', '$http', 'Profile', 'Result', 'Test', 'ProfileResult', 'TestProfile', 'Category', 'TestCategory',
    function($scope, $window, $http, Profile, Result, Test, ProfileResult, TestProfile, Category, TestCategory) {
        $scope.pesel = '';
        $scope.profiles = Profile.query();
        $scope.tests = Test.query();
        $scope.testprofiles = TestProfile.query();
        $scope.profileresults = ProfileResult.query();
        $scope.categories = Category.query();
        $scope.testcategorys = TestCategory.query();
        $scope.activeProfile = {};
        $scope.newProfile = new Profile();
        $scope.labelColor = {"color": "black"};

        $scope.newProfileButtonText = 'Dodaj nowy profil';
        $scope.activatedText = 'Profil nieaktywny';
        $scope.newProfileButtonClicked = false;
        $scope.activeVisible = false;
        $scope.testInProfile = {};
        $scope.profileTransportData = {};
        $scope.isActivated = false;

        $scope.selectProfile = function (hash) {
            $scope.activeProfile = hash;

            if ($scope.activeProfile.hashname !== '') {
                $scope.activeVisible = true;
            }

            // EVERYTHING SET ON FALSE
            for (var i = 0; i < $scope.tests.length; i++) {
                $scope.testInProfile[$scope.tests[i].id] = { value: false, name: $scope.tests[i].name };
            }

            // SET ON TRUE IF IN DATABASE
            for (var i = 0; i < $scope.testprofiles.length; i++) {
                if ($scope.testprofiles[i].profile === $scope.activeProfile.id)
                    $scope.testInProfile[$scope.testprofiles[i].test].value = true;
            }

            // CHECK IF IT'S ACTIVATED
            for (var i = 0; i < $scope.profileresults.length; i++) {
                if ($scope.profileresults[i].profile === $scope.activeProfile.id) {
                    $scope.isActivated = true;
                    $scope.labelColor = {"color": "green"};
                    $scope.activatedText = 'Profil aktywny';
                    return;
                }

                $scope.isActivated = false;
                $scope.labelColor = {"color": "red"};
                $scope.activatedText = 'Profil nieaktywny';
            }
        };

        $scope.newProfileButtonChange = function() {
            if ($scope.newProfileButtonText === 'Dodaj nowy profil') {
                $scope.newProfileButtonText = 'Ukryj dodawanie';
                $scope.newProfileButtonClicked = true;
            } else {
                $scope.newProfileButtonText = 'Dodaj nowy profil';
                $scope.newProfileButtonClicked = false;
                $scope.newProfile = new Profile();
            }
        };

        $scope.postProfile = function() {
            $scope.newProfile.hash = CryptoJS.MD5($scope.pesel + $scope.newProfile.name).toString();

            $scope.newProfile.$save(function() {
                // IF SUCCEED
                $window.$.notify('Dodano profil ' + $scope.newProfile.name, 'success');

                $scope.profiles.push($scope.newProfile);
                $scope.selectProfile($scope.newProfile);
                $scope.newProfile = new Profile();
                $scope.newProfileButtonChange();
            }, function() {
                // IF NOT SUCCEED

                $window.$.notify('Dodanie profilu nie powiodło się.', 'error');
            });
        };
        $scope.deleteProfile = function() {
            $scope.activeProfile.$delete(function() {
                // IF SUCCEED
                $window.$.notify('Usunięto profil ' + $scope.activeProfile.name, 'success');

                $scope.profiles = Profile.query();
                $scope.activeProfile = {};
                $scope.activeVisible = false;
                $scope.isActivated = false;
            }, function() {
                // IF NOT SUCCEED
                $window.$.notify('Usuwanie profilu nie powiodło się.', 'error');
            });
        };
        $scope.updateProfile = function() {
            $scope.activeProfile.$update(function () {
                // IF SUCCEED
                $window.$.notify('Zatwierdzono zmianę nazwy ' + $scope.activeProfile.name, 'success');
            }, function() {
                // IF NOT SUCCEED
                $window.$.notify('Zmiana nazwy nie powiodła się.', 'error');
            });
        };

        $scope.changeState = function(id) {
            if ($scope.testInProfile[id].value === false) {
                for (var i = 0; i < $scope.testprofiles.length; i++) {
                    if ($scope.testprofiles[i].profile === $scope.activeProfile.id && $scope.testprofiles[i].test === id) {
                        $scope.testprofiles[i].$delete(function() {
                            $window.$.notify('Usunięto badanie ' + $scope.testInProfile[id].name + ' z kategorii ' + $scope.activeProfile.name, 'success');
                            if ($scope.isActivated)
                                $scope.deactivateProfile();
                        }, function() {
                            $window.$.notify('Nie udało się usunąć badania ' + $scope.testInCategory[id].name + ' do kategorii ' + $scope.activeProfile.name, 'error');
                        });
                        break;
                    }
                }
            } else {
                var newTestProfile = new TestProfile();
                newTestProfile.test = id;
                newTestProfile.profile = $scope.activeProfile.id;
                newTestProfile.$save(function() {
                    $window.$.notify('Dodano badanie ' + $scope.testInProfile[id].name + ' do kategorii ' + $scope.activeProfile.name, 'success');
                    if ($scope.isActivated)
                        $scope.deactivateProfile();
                }, function () {
                    $window.$.notify('Nie udało się dodać badania ' + $scope.testInProfile[id].name + ' do kategorii ' + $scope.activeProfile.name, 'error');
                });
            }

            $scope.testprofiles = TestProfile.query();
        };

        $scope.activateProfile = function(username) {
            $scope.profileTransportData['username'] = username;
            $scope.profileTransportData['profileID'] = $scope.activeProfile.id;
            $scope.profileTransportData['tests'] = [];
            $scope.profileTransportData['activate'] = 'yes';

            for (var testID in $scope.testInProfile) {
                if ($scope.testInProfile[testID].value)
                    $scope.profileTransportData['tests'].push(parseInt(testID));
            }

            var answer = $http.post('/patients/postprofileresults', $scope.profileTransportData);

            answer.success(function() {
                $scope.isActivated = true;
                $scope.labelColor = {"color": "green"};
                $scope.activatedText = 'Profil aktywny';
                $scope.profileresults = ProfileResult.query();
                $window.$.notify('Aktywowano profil ' + $scope.activeProfile.name, 'success');
            });
            answer.error(function() {
                $window.$.notify('Nie udało się aktywować profilu.', 'error');
            });

            $scope.profileTransportData = {};
        };

        $scope.deactivateProfile = function() {
            this.profileTransportData['profileID'] = $scope.activeProfile.id;
            this.profileTransportData['activate'] = 'no';

            var answer = $http.post('/patients/postprofileresults', $scope.profileTransportData);

            answer.success(function() {
                $scope.isActivated = false;
                $scope.labelColor = {"color": "red"};
                $scope.activatedText = 'Profil nieaktywny';
                $scope.profileresults = ProfileResult.query();
                $window.$.notify('Zdezaktywowano profil ' + $scope.activeProfile.name, 'success');
            });
            answer.error(function() {
                $window.$.notify('Nie udało się dezaktywować profilu.', 'error');
            });

            $scope.profileTransportData = {};
        };

        $scope.updateFromCategory = function() {

        };
    }]);

    app.directive('onlyDigits', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9.]/g, '');

                        if (digits.split('.').length > 2)
                            digits = digits.substring(0, digits.length - 1);

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseFloat(digits);
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });

    app.directive('onlyTime', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9:]/g, '');

                        if ((digits.split(':')[0].length > 2) ||
                            (digits.split(':').length > 1 && digits.split(':')[1].length > 2) ||
                            (parseInt(digits.split(':')[0]) > 23) ||
                            (digits.split(':').length > 1 && parseInt(digits.split(':')[1]) > 59) ||
                            (digits.split(':').length > 2) ||
                            (digits.length > 5)
                            )
                            digits = digits.substring(0, digits.length - 1);

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }

                        return digits;
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });

    app.directive('profileInput', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9a-z]/g, '');

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }

                        return digits;
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });
})();

function redirect(url) {
            window.location.replace(url);
        }

$(document).ready(function(){
    $('#mainBody').show();

    $('#edit').on('shown.bs.modal', function () {
        $('#myInput').focus()
    });

    $('table.dataTable').DataTable( {
        'paging': false,
        'info': false,
        'stateSave': true,
        "oLanguage": {
            "sSearch": "Wyszukaj:"
        }
    });

    $('#resultsTable').DataTable( {
        'paging': false,
        'info': false,
        'stateSave': true,
        'dom': 'C<"clear">lfrtip',
        "colVis": {
            "buttonText": "Pokaż/ukryj columny",
            'showAll': 'Pokaż wszystkie'
        },
        "oLanguage": {
            "sSearch": "Wyszukaj:"
        }
    });

    $('.selectpicker').selectpicker({
        style: 'btn-default',
        size: 10
    });

    $.fn.datepicker.dates['pl'] = {
        days: ["Niedziela", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"],
        daysShort: ["Ndz", "Pn", "Wt", "Śr", "Czw", "Pt", "Sob"],
        daysMin: ["Ndz", "Pn", "Wt", "Śr", "Czw", "Pt", "Sob"],
        months: ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"],
        monthsShort: ["Sty", "Lut", "Mar", "Kwi", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru"],
        today: "Dziś",
        clear: "Wyczyść"
    };

    $('.datepicker').datepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        language: 'pl',
    });
});