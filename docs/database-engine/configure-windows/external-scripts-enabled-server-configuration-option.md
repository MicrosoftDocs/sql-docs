---
title: "external scripts enabled Server Configuration Option | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "external scripts enabled"
  - "external_scripts_enabled_TSQL"
helpviewer_keywords: 
  - "external scripts enabled option"
ms.assetid: 9d0ce165-8719-4007-9ae8-00f85cab3a0d
caps.latest.revision: 9
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# external scripts enabled Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Use the **external scripts enabled** option to enable the execution of scripts with certain remote language extensions. This property is OFF by default. When **Advanced Analytics Services** is installed, setup can optionally set this property to true.  
  

 You must enable the external script enabled option before you can execute an external script using the **sp_execute_external_script** procedure. Use **sp_execute_external_script** to execute scripts written in a supported language such as R. In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is comprised of a server component installed with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], and a set of workstation tools and connectivity libraries that connect the data scientist to the high-performance environment of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  Install the **Advanced Analytics Extensions** feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of R scripts. For more information, see [Installing Previous Versions of SQL Server R Services](http://msdn.microsoft.com/library/48380645-9e72-4744-bebb-1c1fd8a18c43).  
  
 To enable external scripts, execute the following script:  
  
```  
sp_configure 'external scripts enabled', 1;  
RECONFIGURE WITH OVERRIDE;  
```  
  
 You must restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to make this change effective.  
  
## See Also  
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)   
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)  
  
  
