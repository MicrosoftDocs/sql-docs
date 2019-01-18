---
title: "Add Account Intelligence to a Dimension | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "dimensions [Analysis Services], Business Intelligence enhancements"
  - "Business Intelligence enhancements [Analysis Services], account intelligence"
  - "account intelligence [Analysis Services]"
ms.assetid: 36f454ae-a9f2-4a59-b19d-40310af9f901
author: minewiskan
ms.author: owend
manager: craigg
---
# Add Account Intelligence to a Dimension
  Add the account intelligence enhancement to a cube or a dimension to assign standard account classifications, such as income and expense, to members of an account attribute. This enhancement also identifies account types (such as Asset and Liability) and assigns the appropriate aggregation to each account type. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] can use the classifications to aggregate accounts over time.  
  
> [!NOTE]  
>  Account intelligence is available only for dimensions that are based on existing data sources. For dimensions that were created without using a data source, you must run the Schema Generation Wizard to create a data source view before adding account intelligence.  
  
 You apply account intelligence to a dimension that specifies account information (for example, account name, account number, and account type). To add account intelligence, you use the Business Intelligence Wizard, and select the **Define account intelligence** option on the **Choose Enhancement** page. This wizard then guides you through the steps of selecting a dimension to which you want to apply account intelligence, identifying account attributes in the selected account dimension, and then mapping account types in the dimension table to account types recognized by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Selecting a Dimension  
 On the first **Define Account Intelligence** page of the wizard, you specify the dimension to which you want to apply account intelligence. The account intelligence enhancement added to this selected dimension will result in changes to the dimension. These changes will be inherited by all cubes that include the selected dimension.  
  
## Specifying Account Attributes  
 On the **Configure Dimension Attributes** page of the wizard, you specify the account attributes in the selected account dimension. First, in the **Include** column, select the check box next to each of the account attribute types that you want to map to a dimension attribute in the dimension. Then, in the **Dimension Attribute** column, expand the drop-down list, and select the attribute in the dimension that corresponds to the selected attribute type. Selecting the attribute from the list sets the attribute `Type` property for the account attributes.  
  
## Mapping Account Types  
 The second **Define Account Intelligence** page maps account type values from the dimension table to account types recognized by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This page appears only if you included the **Account Type** dimension attribute in the dimension. To include the **Account Type** dimension, on the **Define Account Intelligence Settings** page of the wizard, select the check box next to **Account Type**, and then select appropriate attribute.  
  
 On the second **Define Account Intelligence** page, there are two columns:  
  
-   The **Source Table Account Types** column lists the account types that the wizard obtains from the dimension table.  
  
-   The **Server Account Types** column identifies the corresponding account type that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] recognizes. The following table lists the account types that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] recognizes and the default aggregation for each of these types. The selections are made automatically if the dimension table uses the same account type names as [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses.  
  
    |Server account type|Aggregation|Description|  
    |-------------------------|-----------------|-----------------|  
    |**Statistical**|`None`|A calculated ratio of something, or a count of something that does not aggregate over time. This type of account does not convert across currencies with conversion rules.|  
    |**Liability**|`LastNonEmpty`|The money or value of things owed at a specific time. This account type does not accumulate over time, and therefore, does not aggregate naturally over time. For example, the Year amount is the value of the last month with data. This type of account converts across currencies with the End of Period rate.|  
    |**Asset**|`LastNonEmpty`|The money or value of things held at a specific time. This account type accumulates over time, and therefore does not aggregate naturally over time. For example, the Year amount is the value of the last month with data. This type of account converts across currencies with the End of Period rate.|  
    |**Balance**|`LastNonEmpty`|The count of something at a specified time. This account type accumulates but does not aggregate naturally over time. For example, the Year amount is the value of the last month with data.|  
    |**Flow**|`Sum`|An incremental count of something. This account type aggregates as a `Sum` over time but does not convert with currency conversion rules.|  
    |**Expense**|`Sum`|The money or value of things spent. This account type aggregates as a `Sum` over time and converts across currencies with an average rate.|  
    |**Income**|`Sum`|The money or value of things received. This account type aggregates as a `Sum` over time and converts across currencies with an average rate.|  
  
    > [!NOTE]  
    >  If appropriate, you can map more than one account type in the dimension to a particular server account type.  
  
 To change the default aggregations mapped to each account type for a database, you can use the Database Designer.  
  
1.  In Solution Explorer, right-click the Analysis Services project and click **Edit Database**.  
  
2.  In the **Account Type Mapping** box, select an account type in the **Name**.  
  
3.  In the **Alias** text box, type an alias for this account type.  
  
4.  In the **Aggregation Function** drop down list box, change the default Aggregation Function for this account type.  
  
  
