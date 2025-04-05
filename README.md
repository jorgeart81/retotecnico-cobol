# 📄 Documentación de RetoTecnicoCobol CLI

**Versión:** 1.0.0  
**Plataforma:** .NET 9.0  
**Tipo:** Herramienta Global .NET

## 📦 Instalación

### Requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- Terminal (CMD, PowerShell, bash, zsh)

### Instalación Global

```bash
# 1. Empaquetar la aplicación
dotnet pack

# 2. Instalar como herramienta global
dotnet tool install --global --add-source ./nupkg RetoTecnicoCobol

# 3. Verificar instalación
retotecnico-cobol --version
```

## ❌ Desinstalación

```bash
dotnet tool uninstall --global RetoTecnicoCobol
```

## 📂 Estructura del Archivo CSV

La CLI espera archivos CSV con el siguiente formato:

```csv
id,tipo,monto
1,Crédito,100.00
2,Débito,50.00
3,Crédito,200.00
```
