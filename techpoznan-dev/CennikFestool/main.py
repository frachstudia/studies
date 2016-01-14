import sys
import math

def getPriceNetto(id):
    return dictNoPriceNetto[dictIDNo[id]]

def getPriceBrutto(id):
    return dictNoPriceBrutto[dictIDNo[id]]

# numery kolumn w cenniku
CennikProductNumberColumn = 0
CennikNettoPriceColumn = 2
CennikBruttoPriceColumn = 3

# numery kolumn w pliku epp
EppIdColumn = 1
EppNoColumn = 2
EppSecondIdColumn = 0
EppNettoPriceColumn = 2
EppBruttoPriceColumn = 3

# prog wzglednej roznicy, zeby wrzucic do warningList
Threshold = 0.3

# ustawienia
Cennik = 'cennik.csv'
Data = 'fes.epp'
NowyPlik = 'new.epp'

f = open(Cennik)

try:
    txt = f.readlines()
except:
    print("Blad odczytu pliku " + Cennik)
    sys.exit(1)
finally:
    f.close()

dictNoPriceNetto = {}
dictNoPriceBrutto = {}
print("Tworzenie slownika Numer -> Cena . . .")

for line in txt:
    fields = line.split(',')
    # wywalamy wszelkie biale znaki oraz dodajemy na koncu dwa zera (tak musi byc w .epp)
    dictNoPriceNetto[fields[CennikProductNumberColumn]] = ''.join(fields[CennikNettoPriceColumn].split()) + "00"
    dictNoPriceBrutto[fields[CennikProductNumberColumn]] = ''.join(fields[CennikBruttoPriceColumn].split()) + "00"

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
    for index in range(towaryStartIndex,towaryStopIndex):
        IDList.append(text[index])
    for index in range(cenyStartIndex, cenyStopIndex):
        PriceList.append(text[index])

    print("Tworzenie slownika ID -> Numer . . .")
    for index in range(0, len(IDList)):
        fields = IDList[index].split(',')
        dictIDNo[fields[EppIdColumn][1:-1]] = fields[EppNoColumn][1:-1]

    #print(getPriceNetto('FEF225'))

    notPresentList = []
    warningList = []

    print("Zmienianie cen . . .")
    for index in range(cenyStartIndex,cenyStopIndex):
        fields = text[index].split(',')
        id = fields[EppSecondIdColumn][1:-1]
        oldPrice = fields[2]
        try:
            if abs(float(oldPrice) - float(getPriceNetto(id))) > Threshold * float(oldPrice) and id not in warningList:
                warningList.append(id)
                #print("ID=" + id + " WARNING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
                continue
            if id in warningList:
                continue
            #print("ID=" + id + ", Stara cena = " + oldPrice + ", Nowa cena = " + getPriceNetto(id))
            fields[EppNettoPriceColumn] = getPriceNetto(id)
            fields[EppBruttoPriceColumn] = getPriceBrutto(id)
            text[index] = ""
            for ind in range(0,len(fields)):
                text[index] += fields[ind]
                if ind != len(fields) - 1:
                    text[index] += ","
            #print(text[index])

        except KeyError:
            if id not in notPresentList:
                notPresentList.append(id)
                #print("ID=" + id + " nie ma w cenniku")
    print("Zapisywanie zmian do pliku . . .")
    try:
        newFile.writelines(text)
    finally:
        newFile.close()
except:
    print("Blad odczytu pliku " + Data)
    sys.exit(1)
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
