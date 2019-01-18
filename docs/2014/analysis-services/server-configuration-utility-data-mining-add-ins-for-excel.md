---
title: "Server Configuration Utility (Data Mining Add-ins for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 28435f86-5cec-4a1e-9b7d-b2069c1ddddb
author: minewiskan
ms.author: owend
manager: craigg
---
# Server Configuration Utility (Data Mining Add-ins for Excel)
  When you install the Data Mining Add-ins for Excel, a Server Configuration Utility is also installed, and will run the first time you open the add-ins. This topic describes how to use the utility to connect to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and set up a database for working with data mining models.  
  

  
##  <a name="bkmk_step1"></a> Step 1: Connect to Analysis Services  
 Choose the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server that provides the data mining algorithms and stores your data mining models.  
  
 When you create a connection to enable data mining, you should choose a server where you can experiment with data mining models. We recommend that you create a new database on the server and dedicate the new database to data mining, or ask your administrator to prepare a data mining server for you. That way you can build models without affecting the performance of other services.  
  
 Note that the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server that you use for data mining does not need to be on the same server as your source data.  
  
 **Server name**  
 Type the name of the server that contains the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you will use for data mining.  
  
 **Authentication**  
 Specify the authentication methods. Windows Authentication is required for connections to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], unless your administrator has configured access to the server via the HTTPPump.  
  
##  <a name="bkmk_step2"></a> Step 2: Allow Temporary Models  
 Before you can use the add-ins, an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server property must be changed to permit the creation of temporary mining models.  
  
 Temporary mining models are also called *session models*. This is because the models are stored only as long as your current session is open. When you close your connection to the server, the session is ended and any models used during the session are deleted.  
  
 The use of session mining models does not affect storage space on the server, but the overhead involved in creating data mining models may affect the performance of the server.  
  
 The wizard first detects the settings on the server that you specified. If the server already allows temporary mining models, you can click **Next** to continue. The wizard also provides instructions for how to enable temporary mining models on the specified [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server, or how to make a request to your [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] administrator.  
  
##  <a name="bkmk_step3"></a> Step 3: Create Database for Add-in Users  
 On this page of the setup and configuration wizard, you can create a new database that is dedicated to data mining, or select an existing [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
> [!WARNING]  
>  Data mining requires an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that is running in multidimensional mode. Tabular mode does not support data mining.  
  
 We recommend that you create a separate database dedicated to data mining. This lets you experiment with data mining models without affecting other solution objects.  
  
 If you choose an existing database on an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], note that it is possible to overwrite existing models if you use the add-ins to create a model and a model of that name already exists.  
  
 **Create new database**  
 Select this option to create a new database on the selected server. A data mining database will store your data sources, mining structures, and mining models.  
  
 **Database name**  
 Type the name of the new database. If the name is not already in use, it will be created for you.  
  
 **Use existing database**  
 Select this option to use an existing [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 **Database**  
 If you chose the option to use an existing database, you must select the database name from the list.  
  
##  <a name="bkmk_step4"></a> Step 4: Give Add-in Users Appropriate Permissions  
 You must ensure that you (and any other users of the add-ins) have the necessary permissions to browse, edit, process, or create data mining structures and models.  
  
 By default, integrated Windows Authentication is required to use the add-ins.  
  
 **Give add-in users database administration permissions**  
 Select this option to give the listed users administrative access to the database.  
  
> [!NOTE]  
>  These permissions apply only to the database listed in the **Database name** box.  
  
 **Database name**  
 Displays the name of the selected database.  
  
 **Specify users or groups to add**  
 Select the logins that will have access to the database used for data mining.  
  
 **Add**  
 Click to open a dialog box and add users or groups.  
  
 **Remove**  
 Click to remove the selected user or group from the list of users with administration permissions.  
  
  
