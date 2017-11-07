---
title: "Run a Reporting Services Script File | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "scripts [Reporting Services], running"
ms.assetid: 0de4995c-85ec-4d4c-aaef-fbd30edfb20f
caps.latest.revision: 36
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Run a Reporting Services Script File
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] script files are run from the command prompt using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] script environment (RS.exe). RS.exe has many command prompt arguments available for you to use. For more information about the command prompt options, see [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md). For more script samples, see [SQL Server Reporting Services Product Samples](http://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Sample Command Lines  
  
-   Run Script.rss in the script environment designating the target report server. Windows Authentication is applied by default:  
  
    ```  
    rs –i Script.rss -s http://servername/reportserver  
    ```  
  
-   Run Script.rss in the script environment specifying a user name and password for authenticating the Web service calls:  
  
    ```  
    rs –i Script.rss -s http://servername/reportserver -u myusername -p mypassword  
    ```  
  
-   Run Script.rss in the script environment specifying a server time-out of 30 seconds:  
  
    ```  
    rs –i Script.rss -s http://servername/reportserver -l 30  
    ```  
  
-   Run Script.rss in the script environment specifying a global script variable called *report*.  
  
    ```  
    rs –i Script.rss -s http://servername/reportserver -v report="Company Sales"  
    ```  
  
-   Run Script.rss in the script environment specifying that the Web service operations in the script file are run as a batch.  
  
    ```  
    rs –i Script.rss -s http://servername/reportserver -b  
    ```  
  
## See Also  
 [Technical Reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)  
  
  