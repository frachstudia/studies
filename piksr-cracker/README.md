# Distributed OpenCL Cracker

* **Michał Słomkowski 100489**
* **Filip Rachwalak 99523**

## Założenia

* rozproszone łamanie funkcji skrótu w oparciu o akceleratory i OpenCL
* wybór własnych funkcji hashowych
* interfejs administracyjny - możliwość dynamicznego dodawania/usuwania węzłów
* interfejs kliencki - bezpieczeństwo dostępu do usług 

## Struktura

* Client - konsolowy do zarządzania,
* Server - wystawia interfejs HTTP dla klienta i rozdziela pracę dla *Processorów*,
* Processor - dokonuje obliczania hashy.
