---
title: "Element Path Syntax for XML Report Data (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "ElementPath syntax"
  - "XML [Reporting Services], data retrieval"
ms.assetid: 07bd7a4e-fd7a-4a72-9344-3258f7c286d1
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Element Path Syntax for XML Report Data (SSRS)
  In Report Designer, you specify the data to use for a report from an XML data source by defining a case-sensitive element path. An element path indicates how to traverse the XML hierarchical nodes and their attributes in the XML data source. To use the default element path, leave the dataset query or the XML `ElementPath` of the XML `Query` empty. When data is retrieved from the XML data source, element nodes that have text values and element node attributes become columns in the result set. The values of the nodes and attributes become the row data when you run the query. The columns appear as the dataset field collection in the Report Data pane. This topic describes the element path syntax.  
  
> [!NOTE]  
>  Element path syntax is namespace-independent. To use namespaces in an element path, use the XML query syntax that includes an XML `ElementPath` element described in [XML Query Syntax for XML Report Data &#40;SSRS&#41;](report-data-ssrs.md).  
  
 The following table describes conventions used to define an element path.  
  
|Convention|Used for|  
|----------------|--------------|  
|**bold**|Text that must be typed exactly as shown.|  
|&#124; (vertical bar)|Separates syntax items. You can choose only one of the items.|  
|`[ ] (brackets)`|Optional syntax items. Do not type the brackets.|  
|**{ }** (braces)|Delimits parameters of syntax items.|  
|[**,**...*n*]|Indicates the preceding item can be repeated *n* number of times. The occurrences are separated by commas.|  
  
## Syntax  
  
```  
  
Element path ::=  
    ElementNode[/Element path]  
ElementNode ::=  
    XMLName[(Encoding)][{[FieldList]}]  
XMLName ::=  
    [NamespacePrefix:]XMLLocalName  
Encoding ::=  
        HTMLEncoded | Base64Encoded  
FieldList ::=  
    Field[,FieldList]  
Field ::=  
    Attribute | Value | Element | ElementNode  
Attribute ::=  
        @XMLName[(Type)]  
Value ::=  
        @[(Type)]  
Element ::=  
    XMLName[(Type)]  
Type ::=  
        String | Integer | Boolean | Float | Decimal | Date | XML   
NamespacePrefix ::=  
    Identifier that specifies the namespace.  
XMLLocalName :: =  
    Identifier in the XML tag.   
```  
  
## Remarks  
 The following table summarizes element path terms. Examples in the table refer to the example XML document Customers.xml, which is included in the Examples section of this topic.  
  
> [!NOTE]  
>  XML tags are case-sensitive. When you specify an ElementNode in the element path, you must exactly match the XML tags in the data source.  
  
|Term|Definition|  
|----------|----------------|  
|Element path|Defines the sequence of nodes to traverse within the XML document in order to retrieve field data for a dataset with an XML data source.|  
|`ElementNode`|The XML node in the XML document. Nodes are designated by tags and exist in a hierarchical relationship with other nodes. For example, \<Customers> is the root element node. \<Customer> is a subelement of \<Customers>.|  
|`XMLName`|The name of the node. For example, the name of the Customers node is Customers. An `XMLName` can be prefixed with a namespace identifier to uniquely name every node.|  
|`Encoding`|Indicates that the `Value` for this element is encoded XML and needs to be decoded and included as a subelement of this element.|  
|`FieldList`|Defines the set of elements and attributes to use to retrieve data.<br /><br /> If not specified, all attributes and subelements are used as fields. If the empty field list is specified (**{}**), no fields from this node are used.<br /><br /> A `FieldList` may not contain both a `Value` and an `Element` or `ElementNode`.|  
|`Field`|Specifies the data that is retrieved as a dataset field.|  
|`Attribute`|A name-value pair within the `ElementNode`. For example, in the element node \<Customer ID="1">, `ID` is an attribute and `@ID(Integer)` returns "1" as an integer type in the corresponding data field `ID`.|  
|`Value`|The value of the element. `Value` can only be used on the last `ElementNode` in the element path. For example, because \<Return> is a leaf node, if you include it at the end of an element path, the value of `Return {@}` is `Chair`.|  
|`Element`|The value of the named subelement. For example, Customers {}/Customer {}/LastName retrieves values for only the LastName element.|  
|`Type`|The optional data type to use for the field created from this element.|  
|`NamespacePrefix`|`NamespacePrefix` is defined in the XML Query element. If no XML Query element exists, namespaces in the XML `ElementPath` are ignored. If there is an XML Query element, the XML `ElementPath` has an optional attribute `IgnoreNamespaces`. If IgnoreNamespaces is `true`, namespaces in the XML `ElementPath` and the XML document are ignored. For more information, see [XML Query Syntax for XML Report Data &#40;SSRS&#41;](report-data-ssrs.md).|  
  
## Example - No Namespaces  
 The following examples use the XML document Customers.xml. This table shows examples of element path syntax and the results of using the element path in a query that defines a dataset, based on the XML document as a data source.  
  
 Note that when the element path is empty, the query uses the default element path: the first path to a leaf node collection. In the first example, leaving the element path empty is equivalent to specifying the element path /Customers/Customer/Orders/Order. All node value and attributes along the path are returned in the result set, and the node names and attributes names appear as dataset fields.  
  
-   *Empty*  
  
    |Order|Qty|ID|FirstName|LastName|Customer.ID|xmlns|  
    |-----------|---------|--------|---------------|--------------|-----------------|-----------|  
    |Chair|6|1|Bobby|Moore|11|http://www.adventure-works.com|  
    |Table|1|2|Bobby|Moore|11|http://www.adventure-works.com|  
    |Sofa|2|8|Crystal|Hu|20|http://www.adventure-works.com|  
    |EndTables|2|15|Wyatt|Diaz|33|http://www.adventure-works.com|  
  
-   `Customers {}/Customer`  
  
    |FirstName|LastName|ID|  
    |---------------|--------------|--------|  
    |Bobby|Moore|11|  
    |Crystal|Hu|20|  
    |Wyatt|Diaz|33|  
  
-   `Customers {}/Customer {}/LastName`  
  
    |LastName|  
    |--------------|  
    |Moore|  
    |Hu|  
    |Diaz|  
  
-   `Customers {}/Customer {}/Orders/Order {@,@Qty}`  
  
    |Order|Qty|  
    |-----------|---------|  
    |Chair|6|  
    |Table|1|  
    |Sofa|2|  
    |EndTables|2|  
  
-   `Customers {}/Customer/Orders/Order{ @ID(Integer)}`  
  
    |Order.ID|FirstName|LastName|ID|  
    |--------------|---------------|--------------|--------|  
    |1|Bobby|Moore|11|  
    |2|Bobby|Moore|11|  
    |8|Crystal|Hu|20|  
    |15|Wyatt|Diaz|33|  
  
#### XML document: Customers.xml  
 To try out the element path examples in the previous section, you can copy this XML and save it to a URL that is accessible by Report Designer, and then use the XML document as an XML data source: for example, `http://localhost/Customers.xml`.  
  
```  
<?xml version="1.0"?>  
<Customers xmlns="http://www.adventure-works.com">  
   <Customer ID="11">  
      <FirstName>Bobby</FirstName>  
      <LastName>Moore</LastName>  
      <Orders>  
         <Order ID="1" Qty="6">Chair</Order>  
         <Order ID="2" Qty="1">Table</Order>  
      </Orders>  
      <Returns>  
         <Return ID="1" Qty="2">Chair</Return>  
      </Returns>  
   </Customer>  
   <Customer ID="20">  
      <FirstName>Crystal</FirstName>  
      <LastName>Hu</LastName>  
      <Orders>  
         <Order ID="8" Qty="2">Sofa</Order>  
      </Orders>  
      <Returns/>  
   </Customer>  
   <Customer ID="33">  
      <FirstName>Wyatt</FirstName>  
      <LastName>Diaz</LastName>  
      <Orders>  
         <Order ID="15" Qty="2">EndTables</Order>  
      </Orders>  
      <Returns/>  
   </Customer>  
</Customers>  
```  
  
 Alternatively, you can create an XML data source that has no connection string and embed Customers.XML in the query, using the following procedure:  
  
###### To embed Customers.XML in a query  
  
1.  Create an XML data source with a blank connection string.  
  
2.  Create a new dataset for the XML data source.  
  
3.  In the **Dataset Properties** dialog box, click **Query Designer**. The text-based query designer dialog box opens.  
  
4.  In the query pane, enter the following two lines:  
  
     `<Query>`  
  
     `<XmlData>`  
  
5.  Copy Customers.XML and paste the text in the query pane after `<XmlData>`.  
  
6.  In the query pane, delete the first line that you copied from Customers.XML: `<?xml version="1.0"?>`  
  
7.  At the end of the query, add the following two lines:  
  
     `<XmlData>`  
  
     `<Query>`  
  
8.  Click **Run Query** (!).  
  
     The result set displays 4 lines of data with the following columns: `xmlns`, `Customer.ID`, `FirstName`, `LastName`, `ID`, `Qty`, `Order`.  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [XML Connection Type &#40;SSRS&#41;](xml-connection-type-ssrs.md)   
 [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)   
 [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md)  
  
  
