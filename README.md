# 📄 Documentación de RetoTecnicoCobol CLI

**Versión:** 1.0.0  
**Plataforma:** .NET 9.0  
**Tipo:** Herramienta Global .NET

## 1. Introducción

La CLI procesa un archivos CSV con transacciones bancarias y genera un reporte que incluye:

- **Balance Final**: Suma de los montos de las transacciones de tipo "Crédito" menos la suma de los montos de las transacciones de tipo "Débito".
- **Transacción de Mayor Monto**: Identifica el monto de la transacción con el valor más alto y su ID.
- **Conteo de Transacciones**: Número total de transacciones para cada tipo ("Crédito" y "Débito").

## 2. Instalación

### Requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- Terminal (CMD, PowerShell, bash, zsh)

### Instalación Global

````bash
# 1. Empaquetar la aplicación
dotnet pack

# 2. Instalar como herramienta global
dotnet tool install --global --add-source ./nupkg RetoTecnicoCobol

# 3. Verificar instalación
retotecnico-cobol --version

### Ejecución

```bash
# 1. Ejecutar la aplicación
retotecnico-cobol

# 2. Ingrese la ruta del archivo .csv y presione Enter. Por ejemplo:
C:\RetoTecnicoCobol\CsvFiles\transactions.csv

````

### ❌ Desinstalación

```bash
dotnet tool uninstall --global RetoTecnicoCobol
```

### 📂 Estructura del Archivo CSV

La CLI espera archivos CSV con el siguiente formato:

```csv
id,tipo,monto
1,Crédito,100.00
2,Débito,50.00
3,Crédito,200.00
```
## 3. Enfoque y Solución
- Validacion de la ruta del archivos CSV.
- Lectura de archivo CSV.
- Extracción y mapeo de la data según el formato especificado.
- Procesamiento de la data para generar **Balance Final**, **Transacción de Mayor Monto** y **Conteo de Transacciones**
- Impresión de resultados en la consola.

## 4. Estructura del Proyecto

### 🗂️ Directorios Principales
```
RetoTecnicoCobol/
├── src/
│   ├── Models/
│   │   ├── TransactionModel.cs           # Modelo de transacción.
│   ├── Services/
│   │   ├── TransactionReport.cs          # Genera y muestra los reportes en la consola.
│   │   ├── TransactionCalculation.cs     # Calculo de Balance Final, Transacción de Mayor Monto
│   ├── Utils/                             y Conteo de Transacciones.
│       ├── ConsoleHelper.cs
│       ├── ProcessCsv.cs                 # Lectura, extracción y mapeo de data del csv.
│    
├── CsvFiles/
│   ├── transaction.csv/                  # Archivo de ejemplo.
│    
└── README.md
```