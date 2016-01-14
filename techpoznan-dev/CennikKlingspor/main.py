import sys
import math

def getNettoPrice(id):
    return dictNoNettoPrice[dictIDNo[id]]

def getBruttoPrice(id):
    return float(dictNoNettoPrice[dictIDNo[id]]) * 1.23

# numery kolumn w cenniku
CennikProductNumberColumn = 6
CennikNettoPriceColumn = 7

# numery kolumn w pliku epp
EppIdColumn = 1
EppNoColumn = 2
EppSecondIdColumn = 0
EppPriceColumn = 2
EppBruttoPriceColumn = 3

# prog wzglednej roznicy, zeby wrzucic do warningList
Threshold = 0.3

# ustawienia
Cennik = 'cennik.csv'
Data = 'kli.epp'
NowyPlik = 'new.epp'

f = open(Cennik, encoding="iso-8859-2")

try:
    txt = f.readlines()
except IOError as e:
    print("Blad odczytu pliku " + Cennik)
    print(e)
    sys.exit(1)
finally:
    f.close()

dictNoNettoPrice = {}
print("Tworzenie slownika Numer -> Cena . . .")

for line in txt:
    fields = line.split(',')
    # wywalamy wszelkie biale znaki oraz dodajemy na koncu dwa zera (tak musi byc w .epp)
    dictNoNettoPrice[fields[CennikProductNumberColumn]] = ''.join(fields[CennikNettoPriceColumn].split()) + "00"

f = open(Data, encoding="iso-8859-2")
newFile = open(NowyPlik, 'w', encoding="iso-8859-2")
IDList = []
PriceList = []
startedID = False
towaryStartIndex = 0
towaryStopIndex = 0

try:
    text = f.readlines()
    dictIDNo = {}
    print("Zbieranie indeksow towarow w pliku . . .")
    for index in range(0,len(text)):
        if text[index].__contains__("TOWARY"):
            towaryStartIndex = index + 3
        if text[index].__contains__("CENNIK"):
            towaryStopIndex = index - 3
            cenyStartIndex = index + 3
        if text[index].__contains__("GRUPYTOWAROW"):
            cenyStopIndex = index - 3

    print("Tworzenie list pomocniczych . . .")
    for index in range(towaryStartIndex, towaryStopIndex + 1):
        IDList.append(text[index])
    for index in range(cenyStartIndex, cenyStopIndex + 1):
        PriceList.append(text[index])

    print("Tworzenie slownika ID -> Numer . . .")
    for index in range(0, len(IDList)):
        fields = IDList[index].split(',')
        dictIDNo[fields[EppIdColumn][1:-1]] = fields[EppNoColumn][1:-1]
    #print (len(dictIDNo))

    #print(getNettoPrice('FEF225'))

    notPresentList = []
    warningList = []

    print("Zmienianie cen . . .")
    for index in range(cenyStartIndex, cenyStopIndex + 1):
        if (index - cenyStartIndex) % 3 == 0:
            fields = text[index].split(',')
            id = fields[EppSecondIdColumn][1:-1]
            if fields[1] == "\"Detaliczna\"":
                print(id)
            oldPrice = fields[EppPriceColumn]
            if dictIDNo[id] not in dictNoNettoPrice.keys():
                print("Nie ma w cenniku")
                notPresentList.append(id)
                continue
            # LICZBA SZTUK W OPAKOWANIU
            liczbaSztuk = 1
            newPrice = str(float(getNettoPrice(id)) / liczbaSztuk) + "00"
            newBruttoPrice = str(round(float(getBruttoPrice(id)) / liczbaSztuk, 2)) + "0000"
            head, sep, tail = newBruttoPrice.partition('.')
            newBruttoPrice = head + sep + tail[:4]

            try:
                if abs(float(oldPrice) - float(newPrice)) > Threshold * float(oldPrice) and id not in warningList:
                    # print(id + ": staraCena = " + oldPrice + ", nowa = " + newPrice)
                    warningList.append(id)
                    print("ID=" + id + " WARNING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
                    continue
                if id in warningList:
                    continue
                print("ID=" + id + ", Stara cena = " + oldPrice + ", Nowa cena netto = " + newPrice + ", brutto = " + newBruttoPrice)

                fields[EppPriceColumn] = newPrice
                fields[EppBruttoPriceColumn] = newBruttoPrice

                text[index] = ""
                for ind in range(0, len(fields)):
                    text[index] += fields[ind]
                    if ind != len(fields) - 1:
                        text[index] += ","
                #print(text[index])
            except KeyError:
                if id not in notPresentList:
                    notPresentList.append(id)
                    print("ID=" + id + " nie ma w cenniku")

    print("Zapisywanie zmian do pliku . . .")
    try:
        newFile.writelines(text)
    finally:
        newFile.close()
except:
    print("Blad odczytu pliku " + Data)
    sys.exit(2)
finally:
    f.close()

print("------------------------------------")
print("Ile towarow nie bylo w cenniku: " + str(len(notPresentList)))
print("Ostrzezenia: " + str(len(warningList)))
#print ("towaryStart = %d, towaryStop = %d" % (towaryStartIndex, towaryStopIndex))

print("Nie ma w cenniku:")
for item in notPresentList:
	print(item)

print("\nWarning:")
for item in warningList:
	print(item)