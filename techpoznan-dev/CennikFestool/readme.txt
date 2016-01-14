CENNIK:
Cennik musi byc w formacie .csv, najlepiej numer seryjny producenta w pierwszej kolumnie, a cena w trzeciej. Jesli inaczej -> nalezy zmienic w ustawieniach w kodzie na poczatku programu. Cena musi zawierac kropke zamiast przecinka, a kolumny w pliku .csv musza byc oddzielone przecinkiem.

PLIK .EPP:
Zdarza sie, ze w tym pliku sa znaki nowej linii w opisie. Nalezy je usunac w taki sposób, aby kazda linia w sekcji "TOWARY" wygladala podobnie.

WYKONYWANIE SKRYPTU:
Jesli jakies parametry sa do zmiany, zmieniamy je w kodzie na poczatku programu. Odpalamy program w konsoli poprzez wpisanie "python main.py" bez argumentów. Warto pomyslec nad progiem przy wsadzaniu na warningList

USTAWIANIE CENY KARTOTEKOWEJ:
W tym momencie ceny są uaktualnione, natomiast źle wyliczana jest marża i cała reszta, bo cena kartotekowa się wyzerowała (tak jest zawsze przy imporcie towarów z pliku .epp). Aby ją poprawić i ustawić na "Cenę z ostatniej dostawy" należy:

1) Wejść w menu Narzędzia -> Przecena
2) Dodać towary, które się chce (grupowo lub z listy)
3) Kliknąć "Sposób wyliczania nowych cen"
4) Wyliczenie cen sprzedaż -> Z ceny zakupu; Cena zakupu -> Cena z ostatniej dostawy
5) KONIECZNIE USUNĄĆ ZAZNACZENIA Z CEN HURTOWA I SPECJALNA ORAZ ZAZNACZYĆ "Przelicz wszystko narzuty"
6) Klikamy "Wylicz ceny". Ceny "Przed przeceną" i "po przecenie" powinny być takie same
7) "Zapisz nowe ceny"
