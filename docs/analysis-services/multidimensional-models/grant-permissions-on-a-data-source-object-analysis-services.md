---
title: "Grant permissions on a data source object (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Grant permissions on a data source object (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Typically, most users of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] do not require access to the data sources that underlie an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. Users ordinarily just query the data within an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. However, in the context of data mining, such as performing predictions based on a mining model, a user has to join the learned data of a mining model with user-provided data. To connect to the data source that contains the user-provided data, the user uses a Data Mining Extensions (DMX) query that contains either the [OPENQUERY &#40;DMX&#41;](../../dmx/source-data-query-openquery.md) and [OPENROWSET &#40;DMX&#41;](../../dmx/source-data-query-openrowset.md) clause.  
  
 To execute a DMX query that connects to a data source, the user must have access to the data source object within the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. By default, only Server Administrators or Database Administrators have access to data source objects. This means that a user cannot access a data source object unless an administrator grants permissions.  
  
> [!IMPORTANT]  
>  For security reasons, the submission of DMX queries by using an open connection string in the OPENROWSET clause is disabled.  
  
## Set Read permissions to a data source  
 A database role can be granted either no access permissions on a data source object or read permissions.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand **Roles** for the appropriate database in Object explorer, and then click a database role (or create a new database role).  
  
2.  In the **Data Source Access** pane, locate the data source object in the **Data Source** list, and then select the **Read** in the **Access** list for the data source. If this option is unavailable, check the **General** pane to see if Full Control is selected. Full Control is already providing permission, you cannot override permissions on the data source.  
  
## Working With the Connection String Used by a Data Source Object  
 The data source object contains the connection string that is used to connect to the underlying data source. This connection string can specify one of the following:  
  
-   **Specify a user name and password**  
  
     If the connection string that a data source object uses specifies a user name and password, you may want to create multiple data source objects, each with different user accounts. Creating multiple data source objects lets users access certain data source objects and prevents those users from accessing other data source objects. These other data source objects can be used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] itself for processing objects, such as cubes and mining models.  
  
-   **Specify Windows Authentication**  
  
     If the connection string that a data source object uses specifies Windows Authentication, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] must be able to impersonate the client. If the data source is on a remote computer, the two computers must be trusted for impersonation by using Kerberos authentication, or the query will typically fail. See [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md) for more information.  
  
     If the client does not allow for impersonation (through the Impersonation Level property in OLE DB and other client components), [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will try to make an anonymous connection to the underlying data source. Anonymous connections to remote data sources rarely succeed, as most data sources do not accept anonymous connections).  
  
## See Also  
 [Data Sources in Multidimensional Models](../../analysis-services/multidimensional-models/data-sources-in-multidimensional-models.md)   
 [Connection String Properties &#40;Analysis Services&#41;](../../analysis-services/instances/connection-string-properties-analysis-services.md)   
 [Authentication methodologies supported by Analysis Services](../../analysis-services/instances/authentication-methodologies-supported-by-analysis-services.md)   
 [Grant custom access to dimension data &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-custom-access-to-dimension-data-analysis-services.md)   
 [Grant cube or model permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-cube-or-model-permissions-analysis-services.md)   
 [Grant custom access to cell data &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-custom-access-to-cell-data-analysis-services.md)  
  
  
