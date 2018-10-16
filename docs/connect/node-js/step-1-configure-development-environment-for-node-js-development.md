---
title: "Step 1:  Configure development environment for Node.js development | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 2dad01f1-fadf-4ac9-9b4d-26be3d301886
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 1:  Configure development environment for Node.js development
You will need to configure your development environment with the prerequisites in order to develop an application using the Node.js Driver for SQL Server.  The most common method is to use the node package manager (npm) to install the tedious module, but you can download the tedious module directly at [Github](https://github.com/pekim/tedious) if you prefer.  
  
Note that the Node.js Driver uses the TDS protocol, which is enabled by default in SQL Server and Azure SQL Database.  No additional configuration is required.  
  
## Windows  
  
1. **Install Node.js runtime and npm package manager**  
a. Go to [Node.js](https://nodejs.org/en/download/)  
b. Click on the appropriate Windows installer msi link.   
c. Once downloaded, run the msi to install Node.js  
  
2. **Open cmd.exe**  
  
3. **Create a project directory** and navigate to it.    
```  
> mkdir HelloWorld  
> cd HelloWorld  
```  
4. **Create a Node project.**  To retain the defaults during your project creation, press enter until the project is created. At the end of this step, you should see a package.json file in your project directory.  
```  
> npm init  
```  
  
5. **Install tedious module in your project.**  This is the implementation of the TDS protocol which the driver uses to communicate to SQL Server.  
```  
> npm install tedious  
```  
  
## Ubuntu Linux  
  
1.  **Open terminal**  
  
2. **Install Node.js runtime**  
```  
>sudo apt-get install node  
```  
3. **Install npm (node package manager)**  
```  
> sudo apt-get install npm  
```  
4. **Create a project directory** and navigate to it.    
```  
> mkdir HelloWorld  
> cd HelloWorld  
```  
  
5. **Create a Node project.**  To retain the defaults during your project creation, press enter until the project is created. At the end of this step, you should see a package.json file in your project directory.  
```  
> sudo npm init  
```  
  
6. **Install tedious module in your project.**  This is the implementation of the TDS protocol which the driver uses to communicate to SQL Server.  
```  
> sudo npm install tedious  
```  
  
## Mac  
  
1. **Install Node.js runtime and npm package manager**  
a. Go to [Node.js](https://nodejs.org/en/download/)  
b. Click on the appropriate Mac OS installer link.  
c. Once downloaded, run the dmg to install Node.js  
  
2. **Open terminal**  
  
3. **Create a project directory** and navigate to it.    
```  
> mkdir HelloWorld  
> cd HelloWorld  
```  
  
4. **Create a Node project.**  To retain the defaults during your project creation, press enter until the project is created. At the end of this step, you should see a package.json file in your project directory.  
```  
> npm init  
```  
  
5. **Install tedious module in your project.**  This is the implementation of the TDS protocol which the driver uses to communicate to SQL Server.  
```  
> npm install tedious  
```  
  
