---
title: "Unique Particle Attribution Constraint | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
f1_keywords: 
  - "unique particle attribution"
helpviewer_keywords: 
  - "schema collections [SQL Server], unique particle attribution"
  - "XML schema collections [SQL Server], unique particle attribution"
  - "UPA constraint rule"
  - "unique particle attribution constraint rule"
ms.assetid: 6bb879e9-a5ee-402e-94e4-fe8cec5966b0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Unique Particle Attribution Constraint
  In XSD, complex content models are constrained by the unique particle attribution (UPA) constraint rule. This rule requires that each element in an instance document correspond unambiguously to exactly one `<xsd:element>` or `<xsd:any>` particle in its parent's content model. Any schema that contains a type with a potentially ambiguous content model is rejected.  
  
 The most common causes of ambiguity are `<xsd:any>` wildcard characters and particles that have variable occurrence ranges, such as minOccurs < maxOccurs. For example, the following content model is ambiguous, because an <`e1`> element could match either the `<xsd:element>` or the `<xsd:any>` element.  
  
```  
<xsd:element name="root">  
    <xsd:complexType>  
        <xsd:choice>  
            <xsd:element name="e1"/>  
            <xsd:any namespace="##any"/>  
        </xsd:choice>  
    </xsd:complexType>  
</xsd:element>  
```  
  
 The following content model is also ambiguous:  
  
```  
<xsd:element name="root">  
    <xsd:complexType>  
        <xsd:sequence>  
            <xsd:element name="e1" maxOccurs="2"/>  
            <xsd:element name="e2" minOccurs="0"/>  
            <xsd:element name="e1"/>  
        </xsd:sequence>  
    </xsd:complexType>  
</xsd:element>  
```  
  
 Although a document such as `<root><e1/><e2/><e1/></root>` can be validated unambiguously, a document such as `<root><e1/><e1/></root>` cannot, because it is not clear to which `<xsd:element>` the second `<e1/>` corresponds. Even though some documents can be validated unambiguously, the schema will be rejected, because of the potential for ambiguity.  
  
 Note that for a content model to be valid, it must be possible to validate any instance unambiguously without looking ahead. For example, consider the following content model:  
  
```  
<xsd:element name="root">  
    <xsd:complexType>  
        <xsd:choice>  
           <xsd:sequence>  
               <xsd:element name="e1"/>  
               <xsd:element name="e2"/>  
           </xsd:sequence>  
           <xsd:sequence>  
               <xsd:element name="e1"/>  
               <xsd:element name="e3"/>  
           </xsd:sequence>  
       </xsd:choice>  
    </xsd:complexType>  
</xsd:element>  
```  
  
 For a document such as `<root><e1/><e3/></root>`, the sequence `<e1/><e3/>` unambiguously matches the second `<xsd:sequence>`. However, because the `<xsd:element>` to which `<e1/>` corresponds cannot be determined without looking ahead to `<e3/>`, the content model violates the UPA constraint rule.  
  
## Finding More Information  
 The following document is published by the World Wide Web Consortium (W3C) and contains the technical description of the unique particle attribution constraint:  
  
 "XML Schema Part 1: Structures Second Edition, W3C Proposed Edited Recommendation":  
  
-   Section 3.8.6: Constraints on Model Group Schema Components  
  
-   Appendix H: Analysis of the Unique Particle Attribution Constraint (non-normative)  
  
 To see the document, visit [http://www.w3.org/TR/xmlschema-1](https://go.microsoft.com/fwlink/?linkid=48881).  
  
## See Also  
 [XML Schema Collections &#40;SQL Server&#41;](xml-schema-collections-sql-server.md)  
  
  
