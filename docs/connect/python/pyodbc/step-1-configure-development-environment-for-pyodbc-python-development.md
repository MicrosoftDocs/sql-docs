---
title: "Step 1: Configure pyodbc Python development environment | Microsoft Docs"
ms.custom: ""
ms.date: "07/06/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 74e69704-e63c-450b-9207-5c1491d0e0f5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 1: Configure development environment for pyodbc Python development

## Windows  
Connect to SQL Database by using Python - pyodbc on Windows:
  
1. **Download Python installer**.  
  If your machine does not have Python, install it. Go to the [Python download page](https://www.python.org/downloads/windows/) and download the appropriate installer. For example, if you are on a 64-bit machine, download the Python 2.7 or 3.7 (x64) installer.  
  
2. **Install Python**.  Once the installer is downloaded, do the following steps:
a. Double-click the file to start the installer. 
b. Select your language, and agree to the terms. 
c. Follow the instructions on the screen and Python should be installed on your computer. 
d. You can verify that Python is installed by going to `C:\Python27` or `C:\Python37` and run `python -V` or `py -V` (for 3.x) 
      
3. [**Install the Microsoft ODBC Driver for SQL Server on Windows**](../../odbc/windows/system-requirements-installation-and-driver-files.md#installing-microsoft-odbc-driver-for-sql-server)
  
4. **Open cmd.exe as an administrator**     

5. **Install pyodbc using pip - Python package manager** (Replace `C:\Python27\Scripts` with your installed Python path)
```  
> cd C:\Python27\Scripts  
> pip install pyodbc  
```  

  
## Linux 
Connect to SQL Database by using Python - pyodbc:
  
1. **Open terminal**  

2. [**Install Microsoft ODBC Driver for SQL Server on Linux**](../../odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)

3.  **Install pyodbc**  
```  
> sudo -H pip install pyodbc
```
