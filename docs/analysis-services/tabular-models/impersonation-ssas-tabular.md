---
title: "Impersonation in Analysis Services tabular models | Microsoft Docs"
ms.date: 01/21/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Impersonation 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This article provides tabular model authors an understanding of how logon credentials are used by Analysis Services when connecting to a datasource to import and process (refresh) data.  

##  <a name="bkmk_conf_imp_info"></a> Configuring impersonation  
 Where, and in what context a model exists determines how impersonation information is configured. When creating a new model project, impersonation is configured in SQL Server Data Tools (SSDT) when you connect to a data source to import data. Once a model is deployed, impersonation can be configured in model database connection string property by using SQL Server Management Studio (SSMS). For tabular models in Azure Analysis Services, you can use SSMS or the **View as: Script** mode in the browser-based designer to edit the Model.bim file in JSON.
  
##  <a name="bkmk_how_imper"></a> How impersonation is used  
 *Impersonation* is the ability of a server application, such as Analysis Services, to assume the identity of a client application. Analysis Services runs using a service account, however, when the server establishes a connection to a datasource, it uses impersonation so that access checks for data import and processing can be performed.  
  
 Credentials used for impersonation are different from the credentials you are currently logged on with. Logged on user credentials are used for particular client-side operations when authoring a model.  
  
 It is important to understand how impersonation credentials are specified and secured, as well as the difference between contexts in which both your logged on user credentials are used and when other impersonation credentials are used.  
  
 **Understanding server-side credentials**  
 
When data is imported or processed, impersonation credentials are used to connect to the datasource and fetch the data. This is a *server-side* operation running in the context of a client application because the Analysis Services server hosting the workspace database connects to the datasource and fetches the data.  
  
 When you deploy a model to an Analysis Services server, if the workspace database is in memory when the model is deployed, the credentials are passed to the Analysis Services server to which the model is deployed. At no time are user credentials stored on-disk.  
  
 When a deployed model processes data from a datasource, the impersonation credentials, persisted in the in-memory database, are used to connect to the datasource and fetch the data. Because this process is handled by the Analysis Services server managing the model database, this is again a server-side operation.  
  
 **Understanding client-side credentials**  
  
 When authoring a new model or adding a datasource to an existing model, you connect to a datasource and select tables and views to be imported into the model. In the Table Import Wizard or Get Data\Query Designer preview and filter features, you see a sample of the data you import. You can also specify filters to exclude data that does not need to be included in the model.  
  
 Similarly, for existing models that have already been created, you use the **Table Properties** dialog to preview and filter data imported into a table.  
  
 The preview and filter features, **Table Properties**, and **Partition Manager** dialog boxes are an in-process *client-side* operation; that is, what is done during this operation are different from how the datasource is connected to and data is fetched from the datasource; a server-side operation. The credentials used to preview and filter data are the credentials of the user currently logged on. In-effect, your credentials. 
  
 The separation of credentials used during server-side and client-side operations can lead to a mismatch in what you see and what data is fetched during an import or process (a server-side operation). If the credentials you are currently logged on with and the impersonation credentials specified are different, the data you see in the preview and filter features or the **Table Properties** dialog and the data fetched during an import or process can be different, depending on the credentials required by the datasource.  
  
> [!IMPORTANT]  
>  When authoring a model, ensure the credentials you are logged on with and the credentials specified for impersonation have sufficient rights to fetch the data from the datasource.  

  
##  <a name="bkmk_imp_info_options"></a> Options  
 When configuring impersonation, or when editing properties for an existing datasource connection, specify one of the following options:  
  
**Tabular 1400 and higher models**
 
|Option|Description|  
|------------|-----------------|  
|**Impersonate Account**|Specifies the model use a Windows user account to import or process data from the datasource. The domain and name of the user account uses the following format:**\<Domain name>\\<User account name\>**.|  
|**Impersonate Current User**|Specifies data should be accessed from the datasource using the identity of the user who sent the request. This mode  applies only to Direct Query mode.|  
|**Impersonate Identity**|Specifies a username to access the datasource, but doesn't need to specify the account's password. This mode applies only when Kerberos delegation is enabled and specifies the S4U authentication should be used.|  
|**Impersonate Service Account**|Specifies the model use the security credentials associated with the Analysis Services service instance that manages the model.|  
|**Impersonate Unattended Account**|Specifies the Analysis Services engine should use a pre-configured unattended account to access the data.|  

> [!IMPORTANT]
> Impersonate Current User is not supported in some environments. For example, Impersonate Current User is not supported for tabular models deployed to Azure Analysis Services that connect to on-premises data sources. This is because an Azure Analysis Services server resource is not connected to an organization's domain. Client credentials cannot be authenticated against the data source server. Azure Analysis Services also does not currently integrate with (Azure) SQL Database support for single sign-on (SSO). Depending on your environment, other impersonation modes also have restrictions. When attempting to use an impersonation mode that is not supported an error is returned. 


**Tabular 1200 models**
 
|Option|Description|  
|------------|-----------------|  
|**Specific Windows user name and password**|This option specifies the model use a Windows user account to import or process data from the datasource. The domain and name of the user account uses the following format:**\<Domain name>\\<User account name\>**. When creating a new model using the Table Import Wizard, this is the default option.|  
|**Service Account**|This option specifies the model use the security credentials associated with the Analysis Services service instance that manages the model.|  
  
##  <a name="bkmk_impers_sec"></a> Security  
 Credentials used with impersonation are persisted in-memory by the VertiPaq™ engine. Credentials are never written to disk. If the workspace database is not in-memory when the model is deployed, the user is be prompted to enter the credentials used to connect to the datasource and fetch data.  
  
> [!NOTE]  
>  It is recommended you specify a Windows user account and password for impersonation credentials. A Windows user account can be configured to use least privileges necessary to connect to and read data from the datasource.  
  

  
## See also  
 [DirectQuery mode](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)   
 [Data sources](../../analysis-services/tabular-models/data-sources-ssas-tabular.md)   
 [Tabular model solution deployment](../../analysis-services/tabular-models/tabular-model-solution-deployment-ssas-tabular.md)  
  
  
