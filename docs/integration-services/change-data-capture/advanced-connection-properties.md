---
title: "Advanced Connection Properties"
description: "Advanced Connection Properties"
author: chugugrace
ms.author: chugu
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
---
# Advanced Connection Properties

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

  Use the **Advanced Connection Properties** dialog box to add more connection parameters to the connection string.  
  
 The additional connection parameters can be any ODBC connection parameter that is supported by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database instance you are using.  
  
 The parameters added using the **Advanced Connection Properties** dialog box are added to the parameters selected in the **Connect to SQL Server** dialog box.  
  
 The last instance of each parameter provided overrides any previous instances of the parameter. Parameters added using the **Advanced Connection Parameters** dialog box follow and replace the parameters provided in the **SQL Server Connection** dialog box. For example, if the **SQL Server Connection** dialog box specifies SERVER1 as the Server name, and the **Additional Connection Parameters** page contains ;SERVER=SERVER2, the connection will be made to SERVER2.  
  
 Parameters added using the **Advanced Connection Properties** dialog box are passed as plain text.  
  
> [!IMPORTANT]  
>  Do not include login credentials in the **Advanced Connection Properties** dialog box. They will not be encrypted when they are passed across the network.  
  
## See Also  
 [Access the CDC Designer Console](../../integration-services/change-data-capture/access-the-cdc-designer-console.md)   
 [SQL Server Connection for Instance Creation](../../integration-services/change-data-capture/sql-server-connection-for-instance-creation.md)  
  
  
