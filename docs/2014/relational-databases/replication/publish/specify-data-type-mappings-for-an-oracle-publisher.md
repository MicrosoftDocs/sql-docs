---
title: "Specify Data Type Mappings for an Oracle Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], data type mapping"
  - "data types [SQL Server replication], Oracle publishing"
  - "mapping data types [SQL Server replication]"
ms.assetid: f172d631-3b8c-4912-bd0f-568366cd9870
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Specify Data Type Mappings for an Oracle Publisher
  This topic describes how to specify data type mappings for an Oracle Publisher in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Although a set of default data type mappings are provided for Oracle Publishers, it might be necessary to specify different mappings for a given publication.  
  
 **In This Topic**  
  
-   **To specify data type mappings for an Oracle Publisher, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Specify data type mappings on the **Data Mapping** tab of the **Article Properties - \<Article>** dialog box. This is available from the **Articles** page of the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication from an Oracle Database](create-a-publication-from-an-oracle-database.md) and [View and Modify Publication Properties](view-and-modify-publication-properties.md).  
  
#### To specify a data type mapping  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table, and then click **Article Properties**.  
  
2.  Click **Set Properties of Highlighted Table Article**.  
  
3.  On the **Data Mapping** tab of the **Article Properties - \<Article>** dialog box, select mappings from the **Subscriber Data Type** column:  
  
    -   For some data types there is only one possible mapping, in which case the column in the property grid is read-only.  
  
    -   For some types, there is more than one type that you can select. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] recommends that you use the default mapping unless your application requires a different mapping. For more information, see [Data Type Mapping for Oracle Publishers](../non-sql/data-type-mapping-for-oracle-publishers.md).  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can specify custom data type mappings programmatically using replication stored procedures. You can also set the default mappings that are used when mapping data types between [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database management system (DBMS). For more information, see [Data Type Mapping for Oracle Publishers](../non-sql/data-type-mapping-for-oracle-publishers.md).  
  
#### To define custom data type mappings when creating an article belonging to an Oracle publication  
  
1.  If one does not already exist, create an Oracle publication.  
  
2.  At the Distributor, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify a value of **0** for **@use_default_datatypes**. For more information, see [Define an Article](define-an-article.md).  
  
3.  At the Distributor, execute [sp_helparticlecolumns](/sql/relational-databases/system-stored-procedures/sp-helparticlecolumns-transact-sql) to view the existing mapping for a column in a published article.  
  
4.  At the Distributor, execute [sp_changearticlecolumndatatype](/sql/relational-databases/system-stored-procedures/sp-changearticlecolumndatatype-transact-sql). Specify the name of the Oracle Publisher for **@publisher**, as well as **@publication**, **@article**, and **@column** to define the published column. Specify the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type to map to for **@type**, as well as **@length**, **@precision**, and **@scale**, where applicable.  
  
5.  At the Distributor, execute [sp_articleview](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql). This creates the view used to generate the snapshot from the Oracle publication.  
  
#### To specify a mapping as the default mapping for a data type  
  
1.  (Optional) At the Distributor on any database, execute [sp_getdefaultdatatypemapping](/sql/relational-databases/system-stored-procedures/sp-getdefaultdatatypemapping-transact-sql). Specify **@source_dbms**, **@source_type**, **@destination_dbms**, **@destination_version**, and any other parameters needed to identify the source DBMS. Information on the currently mapped data type in the destination DBMS is returned using the output parameters.  
  
2.  (Optional) At the Distributor on any database, execute [sp_helpdatatypemap](/sql/relational-databases/system-stored-procedures/sp-helpdatatypemap-transact-sql). Specify **@source_dbms** and any other parameters needed to filter the result set. Note the value of **mapping_id** for the desired mapping in the result set.  
  
3.  At the Distributor on any database, execute [sp_setdefaultdatatypemapping](/sql/relational-databases/system-stored-procedures/sp-setdefaultdatatypemapping-transact-sql).  
  
    -   If you know the desired value of **mapping_id** obtained in step 2, specify it for **@mapping_id**.  
  
    -   If you do not know the **mapping_id**, specify the parameters **@source_dbms**, **@source_type**, **@destination_dbms**, **@destination_type**, and any other parameters required to identify an existing mapping.  
  
#### To find valid data types for a given Oracle data type  
  
1.  At the Distributor on any database, execute [sp_helpdatatypemap](/sql/relational-databases/system-stored-procedures/sp-helpdatatypemap-transact-sql). Specify a value of **ORACLE** for **@source_dbms** and any other parameters needed to filter the result set.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example changes a column with an Oracle data type of NUMBER so it is mapped to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type `numeric`(38,38), instead of the default data type `float`.  
  
 [!code-sql[HowTo#sp_changecolumndatatype](../../../snippets/tsql/SQL15/replication/howto/tsql/datatypemapping.sql#sp_changecolumndatatype)]  
  
 This example query returns the default and alternative mappings for the Oracle 9 data type `CHAR`.  
  
 [!code-sql[HowTo#sp_helpcolumndatatype_char](../../../snippets/tsql/SQL15/replication/howto/tsql/datatypemapping.sql#sp_helpcolumndatatype_char)]  
  
 This example query returns the default mappings for the Oracle 9 data type `NUMBER` when it is specified without a scale or precision.  
  
 [!code-sql[HowTo#sp_helpcolumndatatype_number](../../../snippets/tsql/SQL15/replication/howto/tsql/datatypemapping.sql#sp_helpcolumndatatype_number)]  
  
## See Also  
 [Data Type Mapping for Oracle Publishers](../non-sql/data-type-mapping-for-oracle-publishers.md)   
 [Heterogeneous Database Replication](../non-sql/heterogeneous-database-replication.md)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Configure an Oracle Publisher](../non-sql/configure-an-oracle-publisher.md)  
  
  
