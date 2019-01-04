---
title: "Implementing External Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "disconnected validation [Integration Services]"
  - "connected validation [Integration Services]"
  - "custom data flow components [Integration Services], validating"
  - "validation [Integration Services], components"
  - "metadata [Integration Services]"
  - "data flow components [Integration Services], validating"
  - "data flow components [Integration Services], external metadata"
  - "custom data flow components [Integration Services], external metadata"
  - "external metadata [Integration Services]"
ms.assetid: 8f5bd3ed-3e79-43a4-b6c1-435e4c2cc8cc
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Implementing External Metadata
  When a component is disconnected from its data source, you can validate the columns in the input and output column collections against the columns at its external data source by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100> interface. This interface lets you maintain a snapshot of the columns at the external data source and map these columns to the columns in the input and output column collection of the component.  
  
 Implementing external metadata columns adds a layer of overhead and complexity to component development, because you must maintain and validate against an additional column collection, but the ability to avoid expensive round trips to the server for validation may make this development work worthwhile.  
  
## Populating External Metadata Columns  
 External metadata columns are typically added to the collection when the corresponding input or output column is created. New columns are created by calling the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100.New%2A> method. The properties of the column are then set to match the external data source.  
  
 The external metadata column is mapped to the corresponding input or output column by assigning the ID of the external metadata column to the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.ExternalMetadataColumnID%2A> property of the input or output column. This lets you locate the external metadata column easily for a specific input or output column by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100.GetObjectByID%2A> method of the collection.  
  
 The following example shows how to create an external metadata column and then map the column to an output column by setting the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.ExternalMetadataColumnID%2A> property.  
  
```csharp  
public void CreateExternalMetaDataColumn(IDTSOutput100 output, int outputColumnID )  
{  
    IDTSOutputColumn100 oColumn = output.OutputColumnCollection.GetObjectByID(outputColumnID);  
    IDTSExternalMetadataColumn100 eColumn = output.ExternalMetadataColumnCollection.New();  
  
    eColumn.DataType = oColumn.DataType;  
    eColumn.Precision = oColumn.Precision;  
    eColumn.Scale = oColumn.Scale;  
    eColumn.Length = oColumn.Length;  
    eColumn.CodePage = oColumn.CodePage;  
  
    oColumn.ExternalMetadataColumnID = eColumn.ID;  
}  
```  
  
```vb  
Public Sub CreateExternalMetaDataColumn(ByVal output As IDTSOutput100, ByVal outputColumnID As Integer)   
 Dim oColumn As IDTSOutputColumn100 = output.OutputColumnCollection.GetObjectByID(outputColumnID)   
 Dim eColumn As IDTSExternalMetadataColumn100 = output.ExternalMetadataColumnCollection.New   
 eColumn.DataType = oColumn.DataType   
 eColumn.Precision = oColumn.Precision   
 eColumn.Scale = oColumn.Scale   
 eColumn.Length = oColumn.Length   
 eColumn.CodePage = oColumn.CodePage   
 oColumn.ExternalMetadataColumnID = eColumn.ID   
End Sub  
```  
  
## Validating with External Metadata Columns  
 Validation requires additional steps for components that maintain an external metadata column collection, because you must validate against an additional collection of columns. Validation can be divided into connected validation or disconnected validation.  
  
### Connected Validation  
 When a component is connected to an external data source, the columns in the input or output collections are verified directly against the external data source. Additionally, the columns in the external metadata collection must be validated. This is required because the external metadata collection can be modified by using the **Advanced Editor** in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], and changes made to the collection are not detectable. Therefore, when connected, components must make sure that the columns in the external metadata column collection continue to reflect the columns at the external data source.  
  
 You may choose to hide the external metadata collection in the **Advanced Editor** by setting the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100.IsUsed%2A> property of the collection to `false`. However this also hides the **Column Mapping** tab of the editor, which lets users map columns from the input or output collection to the columns in the external metadata column collection. Setting this property to `false` does not prevent developers from programmatically modifying the collection, but it does provide a level of protection for the external metadata column collection of a component that is used exclusively in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
### Disconnected Validation  
 When a component is disconnected from an external data source, validation is simplified because the columns in the input or output collection are verified directly against the columns in the external metadata collection and not against the external source. A component should perform disconnected validation when the connection to its external data source has not been established, or when the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.ValidateExternalMetadata%2A> property is `false`.  
  
 The following code example demonstrates an implementation of a component that performs validation against its external metadata column collection.  
  
```csharp  
public override DTSValidationStatus Validate()  
{  
    if( this.isConnected && ComponentMetaData.ValidateExternalMetaData )  
    {  
        // TODO: Perform connected validation.  
    }  
    else  
    {  
        // TODO: Perform disconnected validation.  
    }  
}  
```  
  
```vb  
Public  Overrides Function Validate() As DTSValidationStatus   
 If Me.isConnected AndAlso ComponentMetaData.ValidateExternalMetaData Then   
  ' TODO: Perform connected validation.  
 Else   
  ' TODO: Perform disconnected validation.  
 End If   
End Function  
```  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Data Flow](../../data-flow/data-flow.md)  
  
  
