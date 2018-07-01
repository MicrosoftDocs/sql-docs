---
title: "Properties (MDX) | Microsoft Docs"
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
# Properties (MDX)


  Returns a string, or a strongly-typed value, that contains a member property value.  
  
## Syntax  
  
```  
  
Member_Expression.Properties(Property_Name [, TYPED])  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Property_Name*  
 A valid string expression of a member property name.  
  
## Remarks  
 The **Properties** function returns the value of the specified member for the specified member property. The member property can be any of the intrinsic member properties, such as **NAME**, **ID**, **KEY**, or **CAPTION**, or it can be a user-defined member property. For more information, see [Intrinsic Member Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-member-properties-intrinsic-member-properties.md) and [User-Defined Member Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-member-properties-user-defined-member-properties.md).  
  
 By default, the value is coerced to be a string. If **TYPED** is specified, the return value is strongly typed.  
  
-   If the property type is intrinsic, the function returns the original type of the member.  
  
-   If the property type is user defined, the type of the return value is the same as the type of the return value of the **MemberValue** function.  
  
> [!NOTE]  
>  Properties ('Key') returns the same result as Key0 except for composite keys. Properties ('Key') will return null for composite keys. Use the Key*x* syntax for composite keys, as illustrated in the example. Properties ('Key0'), Properties('Key1'), Properties('Key2'), etc collectively form the composite key.  
  
## Example  
 The following example returns both intrinsic and user-defined member properties, utilizing the TYPED argument to return the strongly typed value for the Day Name member property.  
  
```  
WITH MEMBER Measures.MemberName AS   
   [Date].[Calendar].[July 1, 2003].Properties('Name')  
MEMBER Measures.MemberVal AS   
   [Date].[Calendar].[July 1, 2003].Properties('Member_Value')  
MEMBER Measures.MemberKey AS   
   [Date].[Calendar].[July 1, 2003].Properties('Key')  
MEMBER Measures.MemberID AS   
   [Date].[Calendar].[July 1, 2003].Properties('ID')  
MEMBER Measures.MemberCaption AS   
   [Date].[Calendar].[July 1, 2003].Properties('Caption')  
MEMBER Measures.DayName AS   
   [Date].[Calendar].[July 1, 2003].Properties('Day Name', TYPED)  
MEMBER Measures.DayNameTyped AS   
   [Date].[Calendar].[July 1, 2003].Properties('Day Name')  
MEMBER Measures.DayofWeek AS   
   [Date].[Calendar].[July 1, 2003].Properties('Day of Week')  
MEMBER Measures.DayofMonth AS   
   [Date].[Calendar].[July 1, 2003].Properties('Day of Month')  
MEMBER Measures.DayofYear AS   
   [Date].[Calendar].[July 1, 2003].Properties('Day of Year')  
  
SELECT {Measures.MemberName  
   , Measures.MemberVal  
   , Measures.MemberKey  
   , Measures.MemberID  
   , Measures.MemberCaption  
   , Measures.DayName  
   , Measures.DayNameTyped  
   , Measures.DayofWeek  
   , Measures.DayofMonth  
   , Measures.DayofYear  
   }  ON 0  
FROM [Adventure Works]  
```  
  
 The following example shows the use of the KEY*x* property.  
  
```  
WITH   
MEMBER Measures.MemberKey AS   
   [Customer].[Customer Geography].[State-Province].&[QLD]&[AU].Properties('Key')  
MEMBER Measures.MemberKey0 AS   
   [Customer].[Customer Geography].[State-Province].&[QLD]&[AU].Properties('Key0')  
MEMBER Measures.MemberKey1 AS   
   [Customer].[Customer Geography].[State-Province].&[QLD]&[AU].Properties('Key1')  
  
SELECT {Measures.MemberKey  
   , Measures.MemberKey0  
   , Measures.MemberKey1     
   }  ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [Using Member Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-member-properties.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
