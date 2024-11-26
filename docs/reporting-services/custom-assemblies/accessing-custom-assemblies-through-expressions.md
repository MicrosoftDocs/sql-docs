---
title: "Accessing custom assemblies through expressions"
description: After you create a custom assembly, learn how to access classes in your custom assembly by using report expressions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: custom-assemblies
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "expressions [Reporting Services], custom assemblies"
  - "static member calls"
  - "instance member calls [Reporting Services]"
  - "calling class members"
  - "custom assemblies [Reporting Services], expressions"
---
# Accessing custom assemblies through expressions
  After you create a custom assembly, make it available to Report Designer or the report server, add the appropriate security policy, and add a reference to your custom assembly in your report definition, you can access the members of the classes in your assembly using report expressions. To refer to custom code in an expression, you must call the member of a class within the assembly. How you do this depends on whether the method is static or instance-based.  
  
## Call static members from a report definition file  
 Static members belong to the class or type itself and not to an instantiated object. These members can be accessed by directly calling them from the class. You should use static members to call custom functions in a report whenever possible, because static members perform best. To call a static member, you need to reference it as an expression that takes the form =*Namespace.Class.Method*.  
  
### Call static members  
  
-   To call a static member, set your expression equal to the fully qualified name of the member, which includes the namespace, class name, and member name. The following example calls the **ToGBP** method, which converts the **StandardCost** field value from dollars to pounds sterling and displays it in a report:  
  
    ```  
    =CurrencyConversion.DollarCurrencyConversion.ToGBP(Fields!StandardCost.Value)  
    ```  
  
### Important information regarding static fields and properties  
 Currently, all reports are executed in the same application domain. This means that reports with user-specific, static data expose this data to other instances of the same report. This condition might make it possible for the static data of one user to be available to all users currently running a particular report. For this reason, it's highly recommended that you not use static fields or properties in custom assemblies or in the **Code** element; instead, use instance fields or properties in your reports. Static methods can still be used, because they don't store state or data.  
  
## Call instance members from a report definition file  
 If your custom assembly contains instance members that you need to access in a report definition, you must add an instance name for your class to the report. You can add an instance name for a class using the **Code** tab of the **Report Properties** dialog. For more information about adding instances of classes to a report, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
 To call a static member, you need to reference it as an expression that takes the form =Code*.InstanceName.Method*.  
  
### Call instance members  
  
-   To call an instance member of a custom assembly, you must reference the **Code** keyword followed by the instance name and the method. The following example calls an instance method **ToEUR** which converts the **StandardCost** field value from dollars to euros and displays it in a report:  
  
    ```  
    =Code.m_myDollarConversion.ToEUR(Fields!StandardCost.Value)  
    ```  
  
## Related content

- [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)
