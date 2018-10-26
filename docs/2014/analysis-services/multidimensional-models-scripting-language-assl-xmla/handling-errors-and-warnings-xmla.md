---
title: "Handling Errors and Warnings (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "errors [XML for Analysis]"
  - "inline errors [XMLA]"
  - "SOAP faults [XML for Analysis]"
  - "XML for Analysis, errors"
  - "faults [XML for Analysis]"
  - "messages [XML for Analysis]"
  - "XMLA, errors"
  - "warnings [XML for Analysis]"
  - "inline warnings [XMLA]"
ms.assetid: ab895282-098d-468e-9460-032598961f45
author: minewiskan
ms.author: owend
manager: craigg
---
# Handling Errors and Warnings (XMLA)
  Error handling is required when an XML for Analysis (XMLA) [Discover](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover) or [Execute](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method call does not run, runs successfully but generates errors or warnings, or runs successfully but returns results that contain errors.  
  
|Error|Reporting|  
|-----------|---------------|  
|The XMLA method call does not run|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns a SOAP fault message that contains the details of the failure.<br /><br /> For more information, see the section, [Handling SOAP Faults](#handling_soap_faults).|  
|Errors or warnings on a successful method call|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes an [error](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/error-element-xmla) or [warning](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/warning-element-xmla) element for each error or warning, respectively, in the [Messages](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/messages-element-xmla) property of the [root](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/root-element-xmla) element that contains the results of the method call.<br /><br /> For more information, see the section, [Handling Errors and Warnings](#handling_errors_and_warnings).|  
|Errors in the result for a successful method call|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes an inline `error` or `warning` element for the error or warning, respectively, within the appropriate [Cell](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/cell-element-xmla) or [row](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/row-element-xmla) element of the results of the method call.<br /><br /> For more information, see the section, [Handling Inline Errors and Warnings](#handling_inline_errors_and_warnings).|  
  
##  <a name="handling_soap_faults"></a> Handling SOAP Faults  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns a SOAP fault when the following situations occur:  
  
-   The SOAP message that contains the XMLA method was not well-formed or could not be validated by the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
-   A communications or other error occurred involving the SOAP message that contains the XMLA method.  
  
-   The XMLA method did not run on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
 The SOAP fault codes for XMLstartA start with "XMLForAnalysis", followed by a period and the hexadecimal HRESULT result code. For example, an error code of "`0x80000005`" is formatted as "`XMLForAnalysis.0x80000005`". For more information about the SOAP fault format, see Soap Fault in the W3C Simple Object Access Protocol (SOAP) 1.1.  
  
### Fault Code Information  
 The following table shows the XMLA fault code information that is contained in the detail section of the SOAP response. The columns are the attributes of an error in the detail section of a SOAP fault.  
  
|Column name|Type|Description|Null allowed<sup>1</sup>|  
|-----------------|----------|-----------------|------------------------------|  
|`ErrorCode`|`UnsignedInt`|Return code that indicates the success or failure of the method. The hexadecimal value must be converted to an `UnsignedInt` value.|No|  
|`WarningCode`|`UnsignedInt`|Return code that indicates a warning condition. The hexadecimal value must be converted to an `UnsignedInt` value.|Yes|  
|`Description`|`String`|Error or warning text and description returned by the component that generated the error.|Yes|  
|`Source`|`String`|Name of the component that generated the error or warning.|Yes|  
|`HelpFile`|`String`|Path or URL to the Help file or topic that describes the error or warning.|Yes|  
  
 <sup>1</sup> Indicates whether the data is required and must be returned, or whether the data is optional and a null string is allowed if the column does not apply.  
  
 The following is an example of a SOAP fault that occurred when a method call failed:  
  
```  
<?xml version="1.0"?>  
   <SOAP-ENV:Envelope  
   xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/"  
   SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">  
      <SOAP-ENV:Fault>  
         <faultcode>XMLAnalysisError.0x80000005</faultcode>  
         <faultstring>The XML for Analysis provider encountered an error.</faultstring>  
         <faultactor>XML for Analysis Provider</faultactor>  
         <detail>  
<Error  
ErrorCode="2147483653"  
Description="An unexpected error has occurred."  
Source="XML for Analysis Provider"  
HelpFile="" />  
         </detail>  
      </SOAP-ENV:Fault>  
</SOAP-ENV:Envelope>  
```  
  
##  <a name="handling_errors_and_warnings"></a> Handling Errors and Warnings  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns the `Messages` property in the `root` element for a command if the following situations occur after that command runs:  
  
-   The method itself did not fail, but a failure occurred on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance after the method call succeeded.  
  
-   The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance returns a warning when the command is successful.  
  
 The `Messages` property follows all other properties that are contained by the `root` element, and can contain one or more `Message` elements. In turn, each `Message` element can contain either a single `error` or `warning` element describing any errors or warnings, respectively, that occurred for the specified command.  
  
 For more information about errors and warnings that are contained in the `Messages` property, see [Messages Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/messages-element-xmla).  
  
### Handling Errors During Serialization  
 If an error occurs after the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance has already begun serializing the output of a successfully run command, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns an [Exception](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/exception-element-xmla) element in a different namespace at the point of the error. The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance then closes all open elements so that the XML document sent to the client is a valid document. The instance also returns a `Messages` element that contains the description of the error.  
  
##  <a name="handling_inline_errors_and_warnings"></a> Handling Inline Errors and Warnings  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns an inline `error` or `warning` for a command if the XMLA method itself did not fail, but an error specific to a data element in the results returned by the method occurred on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance after the XMLA method call succeeded.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supplies inline `error` and `warning` elements if issues specific to a cell or to other data that are contained within a `root` element using the [MDDataSet](https://docs.microsoft.com/bi-reference/xmla/xml-data-types/mddataset-data-type-xmla) data type occur, such as a security error or formatting error for a cell. In these cases, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns an `error` or `warning` element in the `Cell` or `row` element that contains the error or warning, respectively.  
  
 The following example illustrates a result set that contains an error in the rowset returned from an `Execute` method using the [Statement](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/statement-element-xmla) command.  
  
```  
<return>  
   ...  
   <root>  
      ...  
      <CellData>  
      ...  
         <Cell CellOrdinal="10">  
            <Value>  
               <Error>  
                  <ErrorCode>2148497527</ErrorCode>   
                  <Description>Security Error.</Description>   
               </Error>  
            </Value>  
         </Cell>  
      </CellData>  
      ...  
   </root>  
   ...  
</return>  
```  
  
## See Also  
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
