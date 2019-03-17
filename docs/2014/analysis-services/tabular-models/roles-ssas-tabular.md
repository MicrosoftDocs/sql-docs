---
title: "Roles (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e547382a-c064-4bc6-818c-5127890af334
author: minewiskan
ms.author: owend
manager: craigg
---
# Roles (SSAS Tabular)
  Roles, in tabular models, define member permissions for a model. Each role contains members, by Windows username or by Windows group, and permissions (read, process, administrator). Members of the role can perform actions on the model as defined by the role permission. Roles defined with read permissions can also provide additional security at the row-level by using row-level filters.  
  
> [!IMPORTANT]  
>  In-order for users to connect to a deployed model by using a reporting or data analysis client application, you must create at least one role with at least Read permission to which those users are members.  
  
 Information in this topic is meant for tabular model authors who define roles by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. Roles defined during model authoring apply to the model workspace database. After a model database has been deployed, model database administrators can manage (add, edit, delete) role members using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To learn about managing members of roles in a deployed database, see [Tabular Model Roles &#40;SSAS Tabular&#41;](tabular-model-roles-ssas-tabular.md).  
  
 The [Tabular Modeling &#40;Adventure Works Tutorial&#41;](../tabular-modeling-adventure-works-tutorial.md) includes additional information and lessons on how to use this feature.  
  
 Sections in this topic:  
  
-   [Understanding Roles](#bkmk_underst)  
  
-   [Permissions](#bkmk_permissions)  
  
-   [Row Filters](#bkmk_rowfliters)  
  
-   [Testing Roles](#bkmk_testroles)  
  
-   [Related Tasks](#bkmk_rt)  
  
##  <a name="bkmk_underst"></a> Understanding Roles  
 Roles are used in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to manage security for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and data. There are two types of roles in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
-   The server role, a fixed role that provides administrator access to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   Database roles, roles defined by model authors and administrators to control access to a model database and data for non-administrator users.  
  
 Roles defined for a tabular model are database roles. That is, the roles contain members consisting of Windows users or groups that have specific permissions that define the action those members can take on the model database. A database role is created as a separate object in the database, and applies only to the database in which that role is created. Windows users and/or Windows groups are included in the role by the model author, which, by default, has Administrator permissions on the workspace database server; for a deployed model, by an administrator.  
  
 Roles in tabular models can be further defined with row filters. Row filters use DAX expressions to define the rows in a table, and any related rows in the many direction, that a user can query. Row filters using DAX expressions can only be defined for the Read and Read and Process permissions. For more information, see [Row Filters](#bkmk_rowfliters) later in this topic.  
  
 By default, when you create a new tabular model project, the model project does not have any roles. Roles can be defined by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. When roles are defined during model authoring, they are applied to the model workspace database. When the model is deployed, the same roles are applied to the deployed model. After a model has been deployed, members of the server role ([!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Administrator) and database administrators can manage the roles associated with the model and the members associated with each role by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
> [!NOTE]  
>  Roles defined for a model configured for DirectQuery mode cannot use row filters, however, permissions defined for each role will apply.  
  
##  <a name="bkmk_permissions"></a> Permissions  
 Each role has a single defined database permission (except for the combined Read and Process permission). By default, a new role will have the None permission. That is, once members are added to the role with the None permission, they cannot modify the database, run a process operation, query data, or see the database unless a different permission is granted.  
  
 A Windows group or user can be a member of any number of roles, each role with a different permission. When a user is a member of multiple roles, the permissions defined for each role are cumulative. For example, if a user is a member of a role with the Read permission, and also a member of a role with None permission, that user will have Read permissions.  
  
 Each role can have one the following permissions defined:  
  
|Permissions|Description|Row  filters using DAX|  
|-----------------|-----------------|----------------------------|  
|None|Members cannot make any modifications to the model database schema and cannot query data.|Row filters do not apply. No data is visible to users in this role|  
|Read|Members are allowed to query data (based on row filters) but cannot see the model database in SSMS, cannot make any changes to the model database schema, and the user cannot process the model.|Row filters can be applied. Only data specified in the row filter DAX formula is visible to users.|  
|Read and Process|Members are allowed to query data (based on row-level filters) and run process operations by running a script or package that contains a process command, but cannot make any changes to the database. Cannot view the model database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|Row filters can be applied. Only data specified in the row filter DAX formula can be queried.|  
|Process|Members can run process operations by running a script or package that contains a process command. Cannot modify the model database schema. Cannot query data. Cannot query the model database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|Row filters do not apply. No data can be queried in this role|  
|Administrator|Members can make modifications to the model schema and can query all data in the model designer, reporting client, and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|Row filters do not apply. All data can be queried in this role.|  
  
##  <a name="bkmk_rowfliters"></a> Row Filters  
 Row filters define which rows in a table can be queried by members of a particular role. Row filters are defined for each table in a model by using DAX formulas.  
  
 Row filters can be defined only for roles with Read and Read and Process permissions. By default, if a row filter is not defined for a particular table, members of a role that has Read or Read and Process permission can query all rows in the table unless cross-filtering applies from another table.  
  
 Once a row filter is defined for a particular table, a DAX formula, which must evaluate to a TRUE/FALSE value, defines the rows that can be queried by members of that particular role. Rows not included in the DAX formula will cannot be queried. For example, for members of the Sales role, the Customers table with the following row filters expression, *=Customers [Country] = "USA"*, members of the Sales role, will only be able to see customers in the USA.  
  
 Row filters apply to the specified rows as well as related rows. When a table has multiple relationships, filters apply security for the relationship that is active. Row filters will be intersected with other row filers defined for related tables, for example:  
  
|Table|DAX expression|  
|-----------|--------------------|  
|Region|=Region[Country]="USA"|  
|ProductCategory|=ProductCategory[Name]="Bicycles"|  
|Transactions|=Transactions[Year]=2008|  
  
 The net effect of these permissions on the Transactions table is that members will be allowed to query rows of data where the customer is in the USA, and the product category is bicycles, and the year is 2008. Users would not be able to query any transactions outside of the USA, or any transactions that are not bicycles, or any transactions not in 2008 unless they are a member of another role that grants these permissions.  
  
 You can use the filter, *=FALSE()*, to deny access to all rows for an entire table.  
  
### Dynamic Security  
 Dynamic security provides a way to define row level security based on the user name of the user currently logged on or the CustomData property returned from a connection string. In order to implement dynamic security, you must include in your model a table with login (Windows user name) values for users as well as a field that can be used to define a particular permission; for example, a dimEmployees table with a login ID (domain\username) as well as a department value for each employee.  
  
 To implement dynamic security, you can use the following functions as part of a DAX formula to return the user name of the user currently logged on, or the CustomData property in a connection string:  
  
|Function|Description|  
|--------------|-----------------|  
|[USERNAME Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh230954.aspx)|Returns the domain\ username of the user currently logged on.|  
|[CUSTOMDATA Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh213140.aspx)|Returns the CustomData property in a connection string.|  
  
 You can use the LOOKUPVALUE function to return values for a column in which the Windows user name is the same as the user name returned by the USERNAME function or a string returned by the CustomData function. Queries can then be restricted where the values returned by LOOKUPVALUE match values in the same or related table.  
  
 For example, using this formula:  
  
 `='dimDepartmentGroup'[DepartmentGroupId]=LOOKUPVALUE('dimEmployees'[DepartmentGroupId], 'dimEmployees'[LoginId], USERNAME(), 'dimEmployees'[LoginId], 'dimDepartmentGroup'[DepartmentGroupId])`  
  
 The LOOKUPVALUE function returns values for the dimEmployees[DepartmentId] column where the dimEmployees[LoginId] is the same as the LoginID of the user currently logged on, returned by USERNAME, and values for dimEmployees[DepartmentId] are the same as values for dimDepartmentGroup[DepartmentId]. The values in DepartmentId returned by LOOKUPVALUE are then used to restrict the rows queried in the dimDepartment table, and any tables related by DepartmentId. Only rows where DepartmentId are also in the values for the DepartmentId returned by LOOKUPVALUE function are returned.  
  
 **dimEmployees**  
  
|LastName|FirstName|LoginID|DepartmentName|DepartmentId|  
|--------------|---------------|-------------|--------------------|------------------|  
|Brown|Kevin|Adventure-works\kevin0|Marketing|7|  
|Bradley|David|Adventure-works\david0|Marketing|7|  
|Dobney|JoLynn|Adventure-works\JoLynn0|Production|4|  
|Baretto DeMattos|Paula|Adventure-works\Paula0|Human Resources|2|  
  
 **dimDepartment**  
  
|DepartmentId|DepartmentName|  
|------------------|--------------------|  
|1|Corporate|  
|2|Executive General and Administration|  
|3|Inventory Management|  
|4|Manufacturing|  
|5|Quality Assurance|  
|6|Research and Development|  
|7|Sales and Marketing|  
  
##  <a name="bkmk_testroles"></a> Testing Roles  
 When authoring a model project, you can use the Analyze in Excel feature to test the efficacy of the roles you have defined. From the **Model** menu in the model designer, when you click **Analyze in Excel**, before Excel opens, the **Choose Credentials and Perspective** dialog box appears. In this dialog, you can specify the current username, a different username, a role, and a perspective with which you will use to connect to the workspace model as a data source. For more information, see [Analyze in Excel &#40;SSAS Tabular&#41;](analyze-in-excel-ssas-tabular.md).  
  
##  <a name="bkmk_rt"></a> Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage Roles &#40;SSAS Tabular&#41;](create-and-manage-roles-ssas-tabular.md)|Tasks in this topic describe how to create and manage roles by using the **Role Manager** dialog box.|  
  
## See Also  
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)   
 [Analyze in Excel &#40;SSAS Tabular&#41;](analyze-in-excel-ssas-tabular.md)   
 [USERNAME Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh230954.aspx)   
 [LOOKUPVALUE Function &#40;DAX&#41;](https://msdn.microsoft.com/library/gg492170.aspx)   
 [CUSTOMDATA Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh213140.aspx)  
  
  
