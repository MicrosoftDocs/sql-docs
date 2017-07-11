---
title: "Step 1: Configure pyodbc Python development environment | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 74e69704-e63c-450b-9207-5c1491d0e0f5
caps.latest.revision: 2
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Step 1: Configure development environment for pyodbc Python development
  ## Windows  
  Connect to SQL Database by using Python - pyodbc on Windows
  
1. **Download Python installer**  
  If your machine does not have Python please install it. Go the [Python download page](https://www.python.org/downloads/windows/) and download the appropriate installer. For example if you are on a 64 bit machine, download the Python 2.7 or 3.5 (x64) installer.  
  
2. **Install Python**  Once the installer is downloaded, do the following:
a. Double-click the file to start the installer. 
b. Select your language, and agree to the terms. 
c. Follow the instructions on the screen and Python should be installed on your computer. 
d. You can verify that is Python is installed by going to C:\Python27 or C:\Python35 and run python -v or py -v(for 3.x) 
      
3. [**Install the Microsoft ODBC Driver**](https://docs.microsoft.com/en-us/sql/connect/odbc/download-odbc-driver-for-sql-server)  
  
4. **Open cmd.exe as an administrator**     

5. **Install pyodbc using pip - Python package manager**
```  
> cd C:\Python27\Scripts>  
> pip install pyodbc  
```  

  
## Linux 
Connect to SQL Database by using Python - pyodbc on Ubuntu and RedHat 
  
1. **Open terminal**  

2. **Install Microsoft ODBC Driver 13 for Linux** 
  For Ubuntu 15.04 + 
``` 
> sudo su  
> wget https://gallery.technet.microsoft.com/ODBC-Driver-13-for-Ubuntu-b87369f0/file/154097/2/installodbc.sh  
> sh installodbc.sh  
```   

  For RedHat 6,7 
``` 
> sudo su 
> wget https://gallery.technet.microsoft.com/ODBC-Driver-13-for-SQL-8d067754/file/153653/4/install.sh 
> sh install.sh 
```  
  
3.  **Install pyodbc**  
```  
> sudo -H pip install pyodbc
```  

  
