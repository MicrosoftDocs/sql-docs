---
title: "Accessing Custom Assemblies Through Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "expressions [Reporting Services], custom assemblies"
  - "static member calls"
  - "instance member calls [Reporting Services]"
  - "calling class members"
  - "custom assemblies [Reporting Services], expressions"
ms.assetid: 917c4d47-1a95-4f54-98b1-e8cb2165d90f
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Accessing Custom Assemblies Through Expressions
  Once you have created a custom assembly, made it available to Report Designer or the report server, added the appropriate security policy, and added a reference to your custom assembly in your report definition, you can access the members of the classes in your assembly using report expressions. To refer to custom code in an expression, you must call the member of a class within the assembly. How you do this depends on whether the method is static or instance-based.  
  
## Calling Static Members from a Report Definition File  
 Static members belong to the class or type itself and not to an instantiated object. These members can be accessed by directly calling them from the class. You should use static members to call custom functions in a report whenever possible, because static members perform best. To call a static member, you need to reference it as an expression that takes the form =*Namespace.Class.Method*.  
  
#### To call static members  
  
-   To call a static member, set your expression equal to the fully qualified name of the member, which includes the namespace, class name, and member name. The following example calls the **ToGBP** method, which converts the **StandardCost** field value from dollars to pounds sterling and displays it in a report:  
  
    ```  
    =CurrencyConversion.DollarCurrencyConversion.ToGBP(Fields!StandardCost.Value)  
    ```  
  
### Important Information Regarding Static Fields and Properties  
 Currently, all reports are executed in the same application domain. This means that reports with user-specific, static data expose this data to other instances of the same report. This condition might make it possible for the static data of one user to be available to all users currently running a particular report. For this reason, it is highly recommended that you not use static fields or properties in custom assemblies or in the **Code** element; instead, use instance fields or properties in your reports. Static methods can still be used, because they do not store state or data.  
  
## Calling Instance Members from a Report Definition File  
 If your custom assembly contains instance members that you need to access in a report definition, you must add an instance name for your class to the report. You can add an instance name for a class using the **Code** tab of the **Report Properties** dialog. For more information about adding instances of classes to a report, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
 To call a static member, you need to reference it as an expression that takes the form =Code*.InstanceName.Method*.  
  
#### To call instance members  
  
-   To call an instance member of a custom assembly, you must reference the **Code** keyword followed by the instance name and the method. The following example calls an instance method **ToEUR** which converts the **StandardCost** field value from dollars to euros and displays it in a report:  
  
    ```  
    =Code.m_myDollarCoversion.ToEUR(Fields!StandardCost.Value)  
    ```  
  
## See Also  
 [Using Custom Assemblies with Reports](using-custom-assemblies-with-reports.md)  
  
  
