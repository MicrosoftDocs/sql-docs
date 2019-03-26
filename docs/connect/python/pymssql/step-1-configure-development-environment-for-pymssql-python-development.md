---
title: "Step 1: Configure pymssql Python development environment | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 6d392a5e-b08e-4b35-9e99-61260888fc41
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 1: Configure development environment for pymssql Python development
You will need to configure your development environment with the prerequisites in order to develop an application using the Python Driver for SQL Server.    
  
Note that the Python SQL Drivers use the TDS protocol, which is enabled by default in SQL Server and Azure SQL Database.  No additional configuration is required.  
  
## Windows  
  
1. **Install Python runtime and pip package manager**  
a. Go to [python.org](https://www.python.org/downloads/)  
b. Click on the appropriate Windows installer msi link.   
c. Once downloaded run the msi to install Python runtime  
  
2. **Download pymssql module** from [here](https://www.lfd.uci.edu/~gohlke/pythonlibs/#pymssql)  
  
    Make sure you choose the correct whl file.  For example : If you are using Python 2.7 on a 64 bit machine choose : pymssql‑2.1.1‑cp27‑none‑win_amd64.whl. Once you download the .whl file place it in the C:/Python27 folder.  
      
3. **Open cmd.exe**  
  
4. **Install pymssql module**     
    For example, if you are using Python 2.7 on a 64 bit machine:  
```  
> cd c:\Python27  
> pip install pymssql‑2.1.1‑cp27‑none‑win_amd64.whl  
```  
  
## Ubuntu Linux  
  
1. **Install Python runtime and pip package manager**  Python comes pre-installed on most distributions of Ubuntu.  If your machine does not have python installed, you can get either download the source tarball from [python.org](https://www.python.org/downloads/) and build locally, or you can use the package manager:  
```  
> sudo apt-get install python   
```  
  
2.  **Open terminal**  
  
3.  **Install pymssql module and dependencies**  
```  
> sudo apt-get --assume-yes update  
> sudo apt-get --assume-yes install freetds-dev freetds-bin  
> sudo apt-get --assume-yes install python-dev python-pip  
> sudo pip install pymssql  
```  
  
## Mac  
  
1. **Install Python runtime and pip package manager**  
a. Go to [python.org](https://www.python.org/downloads/)  
b. Click on the appropriate Mac installer pkg link.   
c. Once downloaded run the pkg to install Python runtime  
  
2.  **Open terminal**  
  
3. **Install Homebrew package manager**  
```  
> ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"  
```  
  
4.  **Install FreeTDS module**  
```  
> brew install FreeTDS  
```  
  
5.  **Install pymssql module**  
```  
> sudo -H pip install pymssql  
```
