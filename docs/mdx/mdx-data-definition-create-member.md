---
title: "CREATE MEMBER Statement (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Data Definition - CREATE MEMBER


  Creates a calculated member.  
  
## Syntax  
  
```  
  
CREATE [ SESSION ] [HIDDDEN] [ CALCULATED ] MEMBER CURRENTCUBE | Cube_Name.Member_Name   
   AS MDX_Expression  
      [,Property_Name = Property_Value, ...n]  
......[,SCOPE_ISOLATION = CUBE]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of the cube where the member will be created.  
  
 *Member_Name*  
 A valid string expression that provides a member name. Specify a fully qualified name to create a member within a dimension other than the Measures dimension. If you do not provide a fully qualified member name, the member will be created in the Measures dimension.  
  
 *MDX_Expression*  
 A valid Multidimensional Expressions (MDX) expression.  
  
 *Property_Name*  
 A valid string that provides the name of a calculated member property.  
  
 *Property_Value*  
 A valid scalar expression that defines the calculated member property's value.  
  
## Remarks  
 The CREATE MEMBER statement defines calculated members that are available throughout the session, and therefore, can be used in multiple queries during the session. For more information, see [Creating Session-Scoped Calculated Members &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-calculated-members-session-scoped-calculated-members.md).  
  
 You can also define a calculated member for use by a single query. To define a calculated member that is limited to a single query, you use the WITH clause in the SELECT statement. For more information, see [Creating Query-Scoped Calculated Members &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-calculated-members-query-scoped-calculated-members.md).  
  
 *Property_Name* can refer to either standard or optional calculated member properties. Standard member properties are listed later in this topic. Calculated members created with CREATE MEMBER without a **SESSION** value have session scope. Additionally, strings inside calculated member definitions are delimited with double quotation marks. This is different from the method defined by OLE DB, which specifies that strings should be delimited by single quotation marks.  
  
 Specifying a cube other than the cube that is currently connected causes an error. Therefore, you should use CURRENTCUBE in place of a cube name to denote the current cube.  
  
 For more information about member properties that are defined by OLE DB, see the OLE DB documentation.  
  
## Scope  
 A calculated member can occur within one of the scopes listed in the following table.  
  
 Query scope  
 The visibility and lifetime of the calculated member is limited to the query. The calculated member is defined in an individual query. Query scope overrides session scope. For more information, see [Creating Query-Scoped Calculated Members &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-calculated-members-query-scoped-calculated-members.md).  
  
 Session scope  
 The visibility and lifetime of the calculated member is limited to the session in which it is created. (The lifetime is less than the session duration if a DROP MEMBER statement is issued on the calculated member.) The CREATE MEMBER statement creates a calculated member with session scope.  
  
### Scope Isolation  
 When a cube Multidimensional Expressions (MDX) script contains calculated members, by default the calculated members are resolved before any session-scoped calculations are resolved and before any query-defined calculations are resolved.  
  
> [!NOTE]  
>  In certain scenarios, the [Aggregate (MDX)](../mdx/aggregate-mdx.md) function and the [VisualTotals (MDX)](../mdx/visualtotals-mdx.md) function do not exhibit this behavior.  
  
 The behavior allows generic client applications to work with cubes that contain complex calculations, without having to take into account the specific implementation of the calculations. However, in certain scenarios, you might want to execute session or query-scoped calculated members before certain calculations in the cube, and neither the **Aggregate** function nor the **VisualTotals** function are applicable. To accomplish this, use the SCOPE_ISOLATION calculation property.  
  
#### Example  
 The following script is an example of a scenario where the SCOPE_ISOLATION calculation property is required to produce the correct result.  
  
 **Cube's MDX Script:**  
  
```  
CREATE MEMBER CURRENTCUBE.Measures.ProfitRatio AS 'Measures.[Store Sales]/Measures.[Store Cost]', SOLVE_ORDER = 10  
```  
  
 **MDX Query:**  
  
```  
WITH MEMBER [Customer].[Customers].[USA]. USAWithoutWA AS  
[Customer].[Customers].[Country].&[USA] - [Customer].[Customers].[State Province.&[WA], SOLVE_ORDER=5  
SELECT {USAWithoutWA} ON 0 FROM SALES  
WHERE ProfitRatio  
```  
  
 The desired result of the previous query is the ratio of sales for USA without WA, to store cost for USA without WA. The previous query does not return the desired result; it returns the ratio of USA minus the ratio of WA, which is a meaningless result. To achieve the desired result, you can use the SCOPE_ISOLATION calculation property.  
  
 **MDX Query using the SCOPE_ISOLATION calculation property:**  
  
```  
WITH MEMBER [Customer].[Customers].[USA]. USAWithoutWA AS  
[Customer].[Customers].[Country].&[USA] - [Customer].[Customers].[State Province.&[WA], SOLVE_ORDER=5  
,SCOPE_ISOLATION=CUBE  
SELECT {USAWithoutWA} ON 0 FROM SALES  
WHERE ProfitRatio  
```  
  
## Standard Properties  
 Each calculated member has a set of default properties. When a client application is connected to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the default properties are either supported, or available to be supported, as the administrator chooses.  
  
 Additional member properties may be available, depending upon the cube definition. The following properties represent information relevant to the dimension level in the cube.  
  
|Property identifier|Meaning|  
|-------------------------|-------------|  
|SOLVE_ORDER|The order in which the calculated member will be solved in cases where a calculated member references one other calculated member (that is, where calculated members intersect each other).|  
|FORMAT_STRING|A Office style format string that the client application can use when displaying cell values.|  
|VISIBLE|A value that indicates whether the calculated member is visible in a schema rowset. Visible calculated members can be added to a set with the [AddCalculatedMembers](../mdx/addcalculatedmembers-mdx.md) function. A nonzero value indicates that the calculated member is visible. The default value for this property is *Visible*.<br /><br /> Calculated members that are not visible (where this value is set to zero) are generally used as intermediate steps in more complex calculated members. These calculated members can also be referred to by other types of members, such as measures.|  
|NON_EMPTY_BEHAVIOR|The measure or set that is used to determine the behavior of calculated members when resolving empty cells.<br /><br /> **\*\* Warning \*\*** This property is deprecated. Avoid setting it. See [Deprecated Analysis Services Features in SQL Server 2016](../analysis-services/deprecated-analysis-services-features-in-sql-server-2016.md) for details.|  
|CAPTION|A string that the client application uses as the caption for the member.|  
|DISPLAY_FOLDER|A string that identifies the path of the display folder that the client application uses to show the member. The folder level separator is defined by the client application. For the tools and clients supplied by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the backslash (\\) is the level separator. To provide multiple display folders for a defined member, use a semicolon (;) to separate the folders.|  
|ASSOCIATED_MEASURE_GROUP|The name of the measure group to which this member is associated.|  
  
## See Also  
 [DROP MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-member.md)   
 [UPDATE MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-update-member.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
