---
title: "Run a Reporting Services script file"
description: View examples of how to run a Reporting Services script file from the command prompt by using the Reporting Services script environment (RS.exe).
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "scripts [Reporting Services], running"
---
# Run a Reporting Services script file
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] script files are run from the command prompt by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] script environment (RS.exe). RS.exe has many command prompt arguments available for you to use. For more information about the command prompt options, see [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md). For more script samples, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Sample command lines  
  
-   Run Script.rss in the script environment designating the target report server. Windows Authentication is applied by default:  
  
    ```  
    rs -i Script.rss -s https://servername/reportserver  
    ```  
  
-   Run Script.rss in the script environment specifying a user name and password for authenticating the Web service calls:  
  
    ```  
    rs -i Script.rss -s https://servername/reportserver -u myusername -p mypassword  
    ```  
  
-   Run Script.rss in the script environment specifying a server time-out of 30 seconds:  
  
    ```  
    rs -i Script.rss -s https://servername/reportserver -l 30  
    ```  
  
-   Run Script.rss in the script environment specifying a global script variable called *report*.  
  
    ```  
    rs -i Script.rss -s https://servername/reportserver -v report="Company Sales"  
    ```  
  
-   Run Script.rss in the script environment specifying that the Web service operations in the script file are run as a batch.  
  
    ```  
    rs -i Script.rss -s https://servername/reportserver -b  
    ```  
  
## Related content

- [Technical reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)
