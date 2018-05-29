---
title: "Set Data Source Properties (SSAS Multidimensional) | Microsoft Docs"
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
# Set Data Source Properties (SSAS Multidimensional)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a data source object specifies a connection to an external data warehouse or relational database that provides data to a multidimensional model. Properties on the data source determine the connection string, a timeout interval, maximum number of connections, and the transaction isolation level.  
  
## Set data source properties in SQL Server Data Tools  
  
1.  Double-click a data source in Solution Explorer to open Data Source Designer.  
  
2.  Click the **Impersonation Information** tab in Data Source Designer. For more information about creating a data source, see [Create a Data Source &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/create-a-data-source-ssas-multidimensional.md).  
  
## Set data source properties in Management Studio  
  
1.  Expand the database folder, open the **Data Source** folder under the database name, right-click a data source in **Object Explorer** and select **Properties**.  
  
2.  Optionally, modify the name, description, or impersonation option. For more information, see [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../../analysis-services/multidimensional-models/set-impersonation-options-ssas-multidimensional.md).  
  
## Data Source properties  
  
|Term|Definition|  
|----------|----------------|  
|**Name, ID, Description**|Name, ID, and Description are used to identify and describe the data source object in the multidimensional model.<br /><br /> Name and Description can be specified in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] after you deploy or process the solution.<br /><br /> ID is generated when the object is created. Although you can easily modify the name and description, IDs are read-only and should not be changed. A fixed object ID preserves object dependencies and references throughout the model.|  
|**Create Timestamp**|This read-only property appears in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. It displays the date and time the data source was created.|  
|**Last Schema Update**|This read-only property appears in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. It displays the date and time the metadata for the data source was last updated. This value is updated when you deploy the solution.|  
|**Query Timeout**|Specifies how long a connection request will be attempted before it is dropped.<br /><br /> Type the query timeout in the following format:<br /><br /> *\<Hours>*:*\<Minutes>*:*\<Seconds>*<br /><br /> This property can be overruled by the **DatabaseConnectionPoolTimeoutConnection** server property. If the server property is smaller, it will be used instead of **Query Timeout**.<br /><br /> For more information about the **Query Timeout** property, see <xref:Microsoft.AnalysisServices.DataSource.Timeout%2A>. For more information about the server property, see [OLAP Properties](../../analysis-services/server-properties/olap-properties.md).|  
|**Connection String**|Specifies the physical location of a database that provides data to a multidimensional model, and the data provider used for the connection. This information is provided to a client library making the connection request. The provider determines which properties can be set on the connection string.<br /><br /> The connection string is built using the information you provide in the **Connection Manager** dialog box. You can also view and edit the connection string in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] in the data source property page.<br /><br /> For a SQL Server database, a connection string that contains **user ID** indicates database authentication; a connection containing **Integrated Security=SSPI** indicates Windows authentication.<br /><br /> You can change the server or database name if the database was moved to a new location. Be sure to verify that the credentials currently specified for the connection are mapped to a database login.|  
|**Maximum number of connections**|Specifies the maximum number of connections allowed by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to connect to the data source. If more connections are needed, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will wait until a connection becomes available. The default is 10. Constraining the number of connections ensures that the external data source is not overloaded with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] requests.|  
|**Isolation**|Specifies the locking and row versioning behavior of SQL commands issued by a connection to a relational database. Valid values are ReadCommitted or Snapshot. The default is ReadCommitted, which specifies that data must be committed before it will be read, preventing dirty reads. Snapshot specifies that reads are from a snapshot of previously committed data. For more information about isolation level in SQL Server, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).|  
|**Managed Provider**|Displays the name of the managed provider, such as System.Data.SqlClient or System.Data.OracleClient, if the data source uses a managed provider.<br /><br /> If the data source does not use a managed provider, this property displays an empty string.<br /><br /> This property is read-only in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. To change the provider used on the connection, edit the connection string.|  
|**Impersonation Info**|Specifies the Windows identity that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] runs under when connecting to a data source that uses Windows authentication. Options include using a predefined set of Windows credentials, the service account, the identity of the current user, or an inherit option that can be useful if your model contains multiple data source objects. For more information, see [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../../analysis-services/multidimensional-models/set-impersonation-options-ssas-multidimensional.md).<br /><br /> In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], the valid values list includes these values:<br /><br /> **ImpersonateAccount** (use a specific Windows user name and password to connect to the data source).<br /><br /> **ImpersonateServiceAccount** (use the security identity of the service account to connect to the data source). This is the default value.<br /><br /> **ImpersonateCurrentUser** (use the security identity of the current user to connect to the data source). This option is only valid for data mining queries that retrieve data from an external data warehouse or database; do not choose it for data connections used for processing, loading, or write-back in a multidimensional database.<br /><br /> **Inherit** or **default** (use the impersonation settings of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that contains this data source object). Database properties include impersonation options.|  
  
## See Also  
 [Data Sources in Multidimensional Models](../../analysis-services/multidimensional-models/data-sources-in-multidimensional-models.md)   
 [Create a Data Source &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/create-a-data-source-ssas-multidimensional.md)  
  
  
