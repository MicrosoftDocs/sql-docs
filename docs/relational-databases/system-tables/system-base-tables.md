---
title: "System Base Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "system base tables [SQL Server]"
  - "hobt [SQL Server]"
  - "base tables"
ms.assetid: 31f2df90-651f-4699-8067-19f59b60904f
author: stevestein
ms.author: sstein
manager: craigg
---
# System Base Tables
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  System base tables are the underlying tables that actually store the metadata for a specific database. The **master** database is special in this respect because it contains some additional tables that are not found in any of the other databases. These tables contain persisted metadata that has server-wide scope.  
  
> [!IMPORTANT]  
>  The system base tables are used only within the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and are not for general customer use. They are subject to change and compatibility is not guaranteed.  
  
## System Base-Table Metadata  
 A grantee that has CONTROL, ALTER, or VIEW DEFINITION permission on a database can see system base table metadata in the **sys.objects** catalog view. The grantee can also resolve the names and object IDs of system base tables by using built-in functions such as [OBJECT_NAME](../../t-sql/functions/object-name-transact-sql.md) and [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md).  
  
 To bind to a system base table, a user must connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the dedicated administrator connection (DAC). Trying to execute a SELECT query from a system base table without connecting by using DAC raises an error.  
  
> [!IMPORTANT]  
>  Access to system base tables by using DAC is designed only for [!INCLUDE[msCoName](../../includes/msconame-md.md)] personnel, and it is not a supported customer scenario.  
  
## System Base Tables  
 The following table lists and describes each system base table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Base table|Description|  
|----------------|-----------------|  
|**sys.sysschobjs**|Exists in every database. Each row represents an object in the database.|  
|**sys.sysbinobjs**|Exists in every database. Contains a row for each Service Broker entity in the database. Service Broker entities include the following:<br /><br /> Message type<br /><br /> Service contract<br /><br /> Service<br /><br /> The names and types use binary collation that is fixed.|  
|**sys.sysclsobjs**|Exists in every database. Contains a row for each classified entity that shares the same common properties that include the following:<br /><br /> Assembly<br /><br /> Backup device<br /><br /> Full-text catalog<br /><br /> Partition function<br /><br /> Partition scheme<br /><br /> File group<br /><br /> Obfuscation key|  
|**sys.sysnsobjs**|Exists in every database. Contains a row for each namespace-scoped entity. This table is used for storing XML collection entities.|  
|**sys.syscolpars**|Exists in every database. Contains a row for every column in a table, view, or table-valued function. It also contains rows for every parameter of a procedure or function.|  
|**sys.systypedsubobjs**|Exists in every database. Contains a row for each typed subentity. Only parameters for partition function fall into this category.|  
|**sys.sysidxstats**|Exists in every database. Contains a row for each index or statistics for tables and indexed views<br /><br /> Note: Every index (except heap) is associated with a statistic that has the same name as the index.|  
|**sys.sysiscols**|Exists in every database. Contains a row for each persisted index and statistics column.|  
|**sys.sysscalartypes**|Exists in every database. Contains a row for each user-defined or system type.|  
|**sys.sysdbreg**|Exists in the **master** database only. Contains a row for each registered database.|  
|**sys.sysxsrvs**|Exists in the **master** database only. Contains a row for each local, linked, or remote server.|  
|**sys.sysrmtlgns**|This system base table exists in the **master** database only. Contains a row for each remote login mapping. This is used to map incoming logins that claim to be coming from a corresponding server to an actual local login.|  
|**sys.syslnklgns**|Exists in the **master** database only. Contains a row for each linked login mapping. Linked login mappings are used by remote procedure calls and distributed queries that emanate from a local server out to a corresponding linked server.|  
|**sys.sysxlgns**|Exists in the **master** database only. Contains a row for each server principal.|  
|**sys.sysdbfiles**|Exists in every database. If the column **dbid** is zero, the row represents a file that belongs to this database. In the **master** database, the column **dbid** can be nonzero. When this is the case, the row represents a master file.|  
|**sys.sysusermsg**|Exists in the **master** database only. Each row represents a user-defined error message.|  
|**sys.sysprivs**|Exists in every database. Contains a row for each database- or server-level permission.<br /><br /> Note: Server-level permissions are stored in the **master** database.|  
|**sys.sysowners**|Exists in every database. Each row represents a database principal.|  
|**sys.sysobjkeycrypts**|Exists in every database. Contains a row for each symmetric key, encryption, or cryptographic property associated with an object.|  
|**sys.syscerts**|Exists in every database. Contains a row for each certificate in a database.|  
|**sys.sysasymkeys**|Exists in every database. Each row represents an asymmetric key.|  
|**sys.ftinds**|Exists in every database. Contains a row for each full-text index in the database.|  
|**sys.sysxprops**|Exists in every database. Contains a row for each extended property.|  
|**sys.sysallocunits**|Exists in every database. Contains a row for each storage allocation unit.|  
|**sys.sysrowsets**|Exists in every database. Contains a row for each partition rowset for an index or a heap.|  
|**sys.sysrowsetrefs**|Exists in every database. Contains a row for each index to rowset reference.|  
|**sys.syslogshippers**|Exists in the **master** database only. Contains a row for each database mirroring witness.|  
|**sys.sysremsvcbinds**|Exists in every database. Contains a row for each remote service binding.|  
|**sys.sysconvgroup**|Exists in every database. Contains a row for each service instance in Service Broker.|  
|**sys.sysxmitqueue**|Exists in every database. Contains a row for each Service Broker transmission queue.|  
|**sys.sysdesend**|Exists in every database. Contains a row for each sending endpoint of a Service Broker conversation.|  
|**sys.sysdercv**|Exists in every database. Contains a row for each receiving endpoint of a Service Broker conversation.|  
|**sys.sysendpts**|Exists in the **master** database only. Contains a row for each endpoint created in the server.|  
|**sys.syswebmethods**|Exists in the **master** database only. Contains a row for each SOAP-method defined on a SOAP-enabled HTTP endpoint that is created in the server.|  
|**sys.sysqnames**|Exists in every database. Contains a row for each namespace or qualified name to a 4-byte ID token.|  
|**sys.sysxmlcomponent**|Exists in every database. Each row represents an XML schema component.|  
|**sys.sysxmlfacet**|Exists in every database. Contains a row for each XML facet (restriction) of XML type definition.|  
|**sys.sysxmlplacement**|Exists in every database. Contains a row for each XML placement for XML components.|  
|**sys.syssingleobjrefs**|Exists in every database. Contains a row for each general N-to-1 reference.|  
|**sys.sysmultiobjrefs**|Exists in every database. Contains a row for each general N-to-N reference.|  
|**sys.sysobjvalues**|Exists in every database. Contains a row for each general value property of an entity.|  
|**sys.sysguidrefs**|Exists in every database. Contains a row for each GUID classified ID reference.|  
  
  
