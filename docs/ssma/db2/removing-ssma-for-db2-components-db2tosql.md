---
title: "Removing SSMA for DB2 Components (DB2ToSQL)"
description: "Removing SSMA for DB2 Components (DB2ToSQL)"
author: cpichuka
ms.author: cpichuka
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Removing SSMA for DB2 Components (DB2ToSQL)
When you have finished migrating databases from DB2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you might want to uninstall SSMA components. You can uninstall the client components at any time. However, you should not uninstall the extension pack from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] unless your migrated databases no longer use functions in the **ssma_DB2** schema of the **sysdb** database.  
  
## Uninstalling the SSMA for DB2 Client  
You can uninstall SSMA by using **Add or Remove Programs**.  
  
**To uninstall SSMA**  
  
1.  In Control Panel, open **Add or Remove Programs**.  
  
2.  Select **[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for DB2**, and then click **Remove**.  
  
3.  To confirm that you want to uninstall SSMA, click **Yes**.  
  
## Uninstalling the Extension Pack  
If you are sure your migrated databases do not use objects in the **sysdb.ssma_DB2** schema, you can remove the extension pack by deleting it from the schema - there is no Windows uninstall.  
  
## See Also  
[Installing SSMA for DB2 Client &#40;DB2ToSQL&#41;](../../ssma/db2/installing-ssma-for-db2-client-db2tosql.md)  
[Installing SSMA Components on SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/installing-ssma-components-on-sql-server-db2tosql.md)  
  
