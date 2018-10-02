---
title: "Connect to a Report or Data Feed (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.connreportdatafeed.f1"
ms.assetid: e0ccfb0b-e646-4de8-b7da-f88c986c96e4
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Report or Data Feed (SSAS)
  This page of the **Table Import Wizard** enables you to connect to a data feed. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
## From a Report  
 **Friendly Connection Name**  
 Type a friendly name for the data feed connection.  
  
 **Report Path**  
 Lists the URL for a SQL Server Reporting Services report that generates data feeds. Click **Browse** to specify the URL for the report.  
  
 Click **View Report** to display the report.  
  
 **Browse**  
 Navigate to a location where a report is available.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box..  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection is successful.  
  
## From an Azure DataMarket Dataset  
 **Friendly Connection Name**  
 Type a friendly name for the data feed connection.  
  
 **Data Feed URL**  
 Type the full path to an Atom service document (.atomsvc, .atom) or the URL for a single data feed, or click **Browse** to select an Atom service document.  
  
 **Browse**  
 Navigate to a location where a report is available.  
  
 Click **View available Azure DataMarket datasets** to display available datasets.  
  
 **Account key**  
 Specify the account key used to access your Windows Azure Marketplace dataset subscriptions.  
  
 **Find**  
 Locate an account key associated with a Windows Live account.  
  
 **Save my account key**  
 Saves the account key (encrypted) with the data connection.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box..  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection is successful.  
  
## From Other Feeds  
 **Friendly Connection Name**  
 Type a friendly name for the data feed connection.  
  
 **Data Feed URL**  
 Type the full path to an Atom service document (.atomsvc, .atom) or the URL for a single data feed, or click **Browse** to select an Atom service document.  
  
 Click **Include all feed columns** to specify whether all the data feed columns are imported.  
  
 **Browse**  
 Navigate to a location where a data feed is available.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box.  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection is successful.  
  
  
