##### INSTALLATION #####

1. Packets needed:

	python
	python-psutil
	django
	ntplib

2. Installation (debian):

	sudo apt-get install python2.7
	sudo apt-get install python-pip		#needed for installing django and ntplib
	sudo apt-get install python-psutil
	sudo pip2 instal django
	sudo pip2-intall ntplib


##### RUNNING #####

1. Go into './masterthesis' directory.
2. Run 'manage.py' by command 'python manage.py runserver [port]'.
   default [port] is 8000.
3. If needed, execute 'chmod 777 manage.py'.
4. Type into browser 'http://localhost:8000' (if port number was
   set to default).