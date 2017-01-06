---
title: "Install dwloader Command-Line Loader (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9fd11812-f61b-44fe-bf3a-a688cd23bf2a
caps.latest.revision: 8
author: BarbKess
---
# Install dwloader Command-Line Loader (SQL Server PDW)
Describes how to install the dwloader Command-Line Loader tool for SQL Server PDW.  
  
Parent Topic: [Client Tools and Applications &#40;SQL Server PDW&#41;](../sqlpdw/client-tools-and-applications-sql-server-pdw.md)  
  
## Before You Begin  
Download the **dwloader-x64-pdwau4.msi** Windows Installer file from the [Analytics Platform System Appliance Update 4](https://www.microsoft.com/download/details.aspx?id=48241) page on the Microsoft Download Center.  
  
-   **dwloader** is distributed for 64-bit operating systems only.  
  
-   **dwloader** is distributed only in the en-us (English) locale.  
  
## Install dwloader  
You can install the msi file from the UI or from the command-line. The command-line has an option called CONTROL_NODE_ADDRESS="*control_node_name*" that sets the default for the dwloader –S option. This sets the default appliance that will receive the loaded data. *control_node_name* is a string that specifies the name of the Control node according to the name in the appliance DNS server.  
  
This command installs dwloader, and sets the default for the dwloader –S option to PDW-CTLN. The value "PDW-CTLN" is the name of the Control node that is stored in the appliance DNS server.  
  
```  
msiexec /i ClientTools.msi CONTROL_NODE_ADDRESS="PDW-CTLN"  
```  
  
When running msiexec, the standard options are available. To learn more, run the command `msiexec /?`  
  
For the dwloader reference, see [dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md)  
  
