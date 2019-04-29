---
title: "sys.endpoint_webmethods (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.endpoint_webmethods_TSQL"
  - "sys.endpoint_webmethods"
  - "endpoint_webmethods_TSQL"
  - "sys.http_soap_methods_TSQL"
  - "endpoint_webmethods"
  - "sys.http_soap_methods"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.endpoint_webmethods catalog view"
ms.assetid: 7dad0cf6-eafa-47cf-98cc-75ba8d3c7959
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.endpoint_webmethods (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 Contains a row FOR EACH SOAP method defined on a SOAP-enabled HTTP endpoint. The combination of the endpoint_id and namespace columns is unique.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|endpoint_id|**int**|ID of the endpoint that the webmethod is defined on.|  
|namespace|**nvarchar(384)**|Namespace for the webmethod.|  
|method_alias|**nvarchar(64)**|Alias for the method.<br /><br /> Note: [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers allow characters that are not legal in WSDL method names.<br /><br /> The alias is used to map the name exposed in the WSDL description of the endpoint to the actual underlying [!INCLUDE[tsql](../../includes/tsql-md.md)] executable object that is called when the webmethod is invoked.|  
|object_name|**nvarchar(776)**|The object name that the webmethod is redirected to, as specified in the NAME = option. Name parts are separated by a period (.), and delimited using brackets, `[``]`.<br /><br /> The object name must be a three-part name, as specified in the WSDL option.|  
|result_schema|**tinyint**|Option that determines which, if any, XSD is sent back with a response.<br /><br /> 0 = None<br /><br /> 1 = Standard<br /><br /> 2 = Default|  
|result_schema_desc|**nvarchar(60)**|Description of option that determines which, if any, XSD is sent back with a response.<br /><br /> NONE<br /><br /> STANDARD<br /><br /> DEFAULT|  
|result_format|**tinyint**|Option that determines how results are formatted in the response.<br /><br /> 1 = ALL_RESULTS<br /><br /> 2 = ROWSETS_ONLY<br /><br /> 3 = NONE|  
|result_format_desc|**nvarchar(60)**|Description of the option that determines how results are formatted in the response.<br /><br /> ALL_RESULTS<br /><br /> ROWSETS_ONLY<br /><br /> NONE|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Endpoints Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
