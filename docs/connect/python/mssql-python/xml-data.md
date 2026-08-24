---
title: Use XML Data with mssql-python
description: Learn how to work with XML data in Microsoft SQL using the mssql-python driver, including XQuery, XML methods, and schema collections.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use XML data with mssql-python

Microsoft SQL provides a native `xml` data type with server-side processing capabilities:

- XQuery for querying XML content.
- XML methods (`value()`, `query()`, `exist()`, `nodes()`, `modify()`) for extraction and modification.
- Optional XML schema validation.
- XML indexes for performance.

The mssql-python driver sends and receives XML data as strings. All XML processing (XQuery, XML methods, schema validation) executes on the Microsoft SQL side. Use Python's `xml.etree.ElementTree` or similar libraries for client-side XML parsing.

**When to use XML vs JSON:** Use XML when you need schema validation, namespace support, or mixed content (text interleaved with elements). Use JSON (with `nvarchar` columns and Microsoft SQL's JSON functions) when your data is key-value oriented, consumed by web APIs, or doesn't need schema enforcement. Most new applications prefer JSON unless the data is inherently document-structured.

## Insert XML data

Pass XML as a Python string; the driver sends it to Microsoft SQL's native **xml** column type.

### Insert as string

Directly insert XML content as a string into an xml column using parameterized queries.

```python
import mssql_python
from xml.etree import ElementTree as ET

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes"
)
cursor = conn.cursor()

# Create temp table for XML storage
cursor.execute("CREATE TABLE #XMLOrders (OrderID INT IDENTITY(1,1) PRIMARY KEY, OrderXML XML)")

# Insert XML as string
xml_data = """
<Order OrderID="1001">
    <Customer Name="John Doe" Email="john@example.com"/>
    <Items>
        <Item ProductID="A1" Quantity="2" Price="29.99"/>
        <Item ProductID="B2" Quantity="1" Price="49.99"/>
    </Items>
</Order>
"""

cursor.execute("""
    INSERT INTO #XMLOrders (OrderXML) VALUES (%(xml)s)
""", {"xml": xml_data})
conn.commit()
```

### Build XML with ElementTree

Construct XML documents programmatically using Python's ElementTree library, then convert to a string for insertion.

```python
from xml.etree import ElementTree as ET

# Build XML document
order = ET.Element("Order", OrderID="1002")
customer = ET.SubElement(order, "Customer", Name="Jane Smith", Email="jane@example.com")
items = ET.SubElement(order, "Items")
ET.SubElement(items, "Item", ProductID="C3", Quantity="3", Price="19.99")
ET.SubElement(items, "Item", ProductID="D4", Quantity="2", Price="39.99")

# Convert to string
xml_string = ET.tostring(order, encoding="unicode")

cursor.execute("""
    INSERT INTO #XMLOrders (OrderXML) VALUES (%(xml)s)
""", {"xml": xml_string})
conn.commit()
```

### Validate XML before insert

Validate XML syntax on the server using SQL's TRY/CATCH and XML type casting to reject malformed documents before storage.

```python
# Validate XML with TRY/CATCH
cursor.execute("""
    BEGIN TRY
        DECLARE @xml XML = CAST(%(xml)s AS XML);
        SELECT @xml.value('(/Order/@OrderID)[1]', 'INT') AS Validated;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
""", {"xml": xml_data})
```

## Query XML data

Use XML methods on the server to extract specific values without fetching full documents to the client.

### XML value() method

Extract single scalar values or attributes from XML using the `value()` method with XPath expressions.

Extract scalar values from XML:

```python
cursor.execute("""
    SELECT 
        OrderID,
        OrderXML.value('(/Order/@OrderID)[1]', 'INT') AS XmlOrderID,
        OrderXML.value('(/Order/Customer/@Name)[1]', 'NVARCHAR(100)') AS CustomerName,
        OrderXML.value('(/Order/Customer/@Email)[1]', 'NVARCHAR(100)') AS Email
    FROM #XMLOrders
""")

for row in cursor:
    print(f"Order {row.XmlOrderID}: {row.CustomerName} ({row.Email})")
```

### XML query() method

Return XML fragments (not scalar values) from the document using XPath expressions.

Extract XML fragments:

```python
cursor.execute("""
    SELECT 
        OrderID,
        OrderXML.query('/Order/Items') AS ItemsXML
    FROM #XMLOrders
    WHERE OrderID = %(id)s
""", {"id": 1})

row = cursor.fetchone()
# row.ItemsXML is an XML string
items_xml = row.ItemsXML
print(items_xml)
```

### XML exist() method

Test whether an XPath expression matches any nodes in the document, returning 1 for true and 0 for false.

Check if XPath matches:

```python
cursor.execute("""
    SELECT OrderID, OrderXML
    FROM #XMLOrders
    WHERE OrderXML.exist('/Order/Items/Item[@ProductID="A1"]') = 1
""")

for row in cursor:
    print(f"Order {row.OrderID} contains product A1")
```

### XML nodes() method (shred to rows)

Convert nested XML elements into a relational rowset using the `CROSS APPLY` operator and `nodes()` method.

Convert XML to relational format:

```python
cursor.execute("""
    SELECT 
        o.OrderID,
        Items.Item.value('@ProductID', 'VARCHAR(10)') AS ProductID,
        Items.Item.value('@Quantity', 'INT') AS Quantity,
        Items.Item.value('@Price', 'DECIMAL(10,2)') AS Price
    FROM #XMLOrders o
    CROSS APPLY o.OrderXML.nodes('/Order/Items/Item') AS Items(Item)
    WHERE o.OrderID = %(id)s
""", {"id": 1})

for row in cursor:
    print(f"Product {row.ProductID}: {row.Quantity} x ${row.Price}")
```

## Modify XML data

Use `XML.modify()` with XQuery DML expressions to update XML content in place.

### XML modify() with insert

Add new elements to an XML document using the `modify()` method with XQuery's `insert` operation.

```python
cursor.execute("""
    UPDATE #XMLOrders
    SET OrderXML.modify('
        insert <Item ProductID="E5" Quantity="1" Price="59.99"/>
        into (/Order/Items)[1]
    ')
    WHERE OrderID = %(id)s
""", {"id": 1})
conn.commit()
```

### XML modify() with delete

Remove elements or nodes from an XML document using the `modify()` method with XQuery's `delete` operation.

```python
cursor.execute("""
    UPDATE #XMLOrders
    SET OrderXML.modify('
        delete /Order/Items/Item[@ProductID="A1"]
    ')
    WHERE OrderID = %(id)s
""", {"id": 1})
conn.commit()
```

### XML modify() with replace

Update attribute values or element text in an XML document using the `modify()` method with XQuery's `replace value of` operation.

```python
cursor.execute("""
    UPDATE #XMLOrders
    SET OrderXML.modify('
        replace value of (/Order/Items/Item[@ProductID="B2"]/@Price)[1]
        with 54.99
    ')
    WHERE OrderID = %(id)s
""", {"id": 1})
conn.commit()
```

## FOR XML queries

Append `FOR XML` to any query to return results as a single XML string.

### FOR XML RAW

Generate XML where each row becomes a simple element with columns as attributes.

```python
cursor.execute("""
    SELECT ProductID, Name, ListPrice
    FROM Production.Product
    WHERE ProductSubcategoryID = %(cat)s
    FOR XML RAW('Product'), ROOT('Products')
""", {"cat": 1})

xml_result = cursor.fetchval()
print(xml_result)
# <Products><Product ProductID="1" Name="..." ListPrice="..."/></Products>
```

### FOR XML AUTO

Generate XML with nested structure that automatically reflects the join hierarchy in your query.

```python
cursor.execute("""
    SELECT sc.Name AS SubcategoryName, p.Name AS ProductName, p.ListPrice
    FROM Production.ProductSubcategory sc
    JOIN Production.Product p ON sc.ProductSubcategoryID = p.ProductSubcategoryID
    WHERE sc.ProductSubcategoryID = %(cat)s
    FOR XML AUTO, ROOT('Catalog')
""", {"cat": 1})

xml_result = cursor.fetchval()
# Nested XML structure based on join hierarchy
```

### FOR XML PATH

Construct custom XML structures using explicit column aliases and subqueries to control nesting and element names.

Most control over XML structure:

```python
cursor.execute("""
    SELECT 
        sc.ProductSubcategoryID AS '@ID',
        sc.Name AS 'Name',
        (
            SELECT p.ProductID AS '@ID',
                   p.Name AS 'Name',
                   p.ListPrice AS 'Price'
            FROM Production.Product p
            WHERE p.ProductSubcategoryID = sc.ProductSubcategoryID
            FOR XML PATH('Product'), TYPE
        ) AS 'Products'
    FROM Production.ProductSubcategory sc
    WHERE sc.ProductSubcategoryID = %(cat)s
    FOR XML PATH('Subcategory'), ROOT('Catalog')
""", {"cat": 1})

xml_result = cursor.fetchval()
```

## Parse XML in Python

Retrieve XML as a string and parse it with `xml.etree.ElementTree` or a compatible library.

### Parse query results with ElementTree

Fetch XML from the database and parse it into a Python object tree using ElementTree, accessing elements and attributes via the DOM.

```python
from xml.etree import ElementTree as ET

cursor.execute("""
    SELECT CatalogDescription FROM Production.ProductModel
    WHERE CatalogDescription IS NOT NULL AND ProductModelID = 19
""")

row = cursor.fetchone()
root = ET.fromstring(row.CatalogDescription)

# Navigate XML structure using namespace
ns = {'pd': 'http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription'}
summary = root.find('.//pd:Summary', ns)
if summary is not None:
    # Get all text content
    text = ''.join(summary.itertext()).strip()
    print(f"Summary: {text[:100]}")
```

### Convert XML to dictionary

Transform an XML tree structure into a nested Python dictionary for easier programmatic access to nested data.

```python
def xml_to_dict(element):
    """Convert XML element to dictionary."""
    result = {}
    
    # Add attributes
    if element.attrib:
        result['@attributes'] = element.attrib
    
    # Add children
    children = list(element)
    if children:
        child_dict = {}
        for child in children:
            child_result = xml_to_dict(child)
            if child.tag in child_dict:
                # Convert to list if multiple same-named children
                if not isinstance(child_dict[child.tag], list):
                    child_dict[child.tag] = [child_dict[child.tag]]
                child_dict[child.tag].append(child_result)
            else:
                child_dict[child.tag] = child_result
        result.update(child_dict)
    
    # Add text content
    if element.text and element.text.strip():
        result['#text'] = element.text.strip()
    
    return result

# Usage
cursor.execute("""
    SELECT CatalogDescription FROM Production.ProductModel
    WHERE CatalogDescription IS NOT NULL AND ProductModelID = 19
""")
xml_string = cursor.fetchval()
root = ET.fromstring(xml_string)
data = xml_to_dict(root)
```

## Handle large XML documents

Microsoft SQL might split large `FOR XML` results across multiple rows; concatenate the parts before parsing.

### Fetch XML in chunks

When `FOR XML` returns large result sets, Microsoft SQL might split the XML across multiple rows; combine all parts into a single document.

```python
# FOR XML might split large results across rows
cursor.execute("""
    SELECT * FROM LargeTable FOR XML RAW
""")

xml_parts = []
for row in cursor:
    xml_parts.append(row[0])

full_xml = "".join(xml_parts)
```

### Stream XML parsing

For very large XML documents, use iterative parsing to process elements one at a time without loading the entire tree into memory.

```python
from xml.etree import ElementTree as ET
import io

cursor.execute("""
    SELECT Instructions FROM Production.ProductModel
    WHERE Instructions IS NOT NULL AND ProductModelID = 7
""")
xml_string = cursor.fetchval()

# Use iterparse for memory-efficient parsing
xml_stream = io.StringIO(xml_string)
for event, elem in ET.iterparse(xml_stream, events=['end']):
    if elem.tag.endswith('step'):
        # Process step element
        text = ''.join(elem.itertext()).strip()
        if text:
            print(f"Step: {text[:60]}")
        # Free memory
        elem.clear()
```

## XML indexes

Create indexes in Microsoft SQL for faster XML queries:

```sql
-- Primary XML index (assumes a table with an XML column)
CREATE PRIMARY XML INDEX PIX_XMLOrders_OrderXML
ON #XMLOrders(OrderXML);

-- Secondary indexes for specific access patterns
CREATE XML INDEX SIX_XMLOrders_Path
ON #XMLOrders(OrderXML)
USING XML INDEX PIX_XMLOrders_OrderXML
FOR PATH;

CREATE XML INDEX SIX_XMLOrders_Value
ON #XMLOrders(OrderXML)
USING XML INDEX PIX_XMLOrders_OrderXML
FOR VALUE;
```

## Work with namespaces

Declare namespace prefixes inline in XQuery expressions using the `declare namespace` syntax.

### Query XML with namespaces

Add namespace declarations to your XPath expressions to match elements in a specific namespace.

```python
xml_with_ns = """
<Order xmlns="http://example.com/orders" 
       xmlns:c="http://example.com/customer">
    <c:Customer Name="John Doe"/>
    <Items>
        <Item ProductID="A1"/>
    </Items>
</Order>
"""

cursor.execute("""
    SELECT OrderXML.value('
        declare namespace o="http://example.com/orders";
        declare namespace c="http://example.com/customer";
        (/o:Order/c:Customer/@Name)[1]
    ', 'NVARCHAR(100)') AS CustomerName
    FROM #XMLOrders
    WHERE OrderID = %(id)s
""", {"id": 1})
```

### Parse namespaced XML in Python

When parsing XML with namespaces in Python, declare the namespace mappings in your `find()` and other element access calls.

```python
from xml.etree import ElementTree as ET

cursor.execute("""
    SELECT TOP 1 
        (SELECT SalesOrderID AS [@OrderID],
                TotalDue AS [@Total],
                CustomerID AS [Customer/@ID]
         FROM Sales.SalesOrderHeader
         WHERE SalesOrderID = 43659
         FOR XML PATH('Order'))
""")
xml_string = cursor.fetchval()
root = ET.fromstring(xml_string)
order_id = root.get('OrderID')
total = root.get('Total')
print(f"Order {order_id}: ${total}")
```

## Best practices

Apply these guidelines to work with XML data efficiently.

### Choose XML versus JSON

Decide whether to use Microsoft SQL's native `xml` type or store JSON in `nvarchar` based on your data structure and processing requirements:

This comparison helps you decide whether to use Microsoft SQL's `xml` type or store JSON in `nvarchar`:

| Feature | XML | JSON |
| --------- | ----- | ------ |
| Schema validation | Native support | No native support |
| Namespaces | Full support | No support |
| Attributes | Supported | No direct equivalent |
| Mixed content | Supported | Not supported |
| Document processing | Better | Less structured |

### Avoid SELECT *

Don't retrieve full XML documents when you only need specific values. Use XML methods to extract data on the server. This approach reduces network traffic and avoids parsing large XML documents in Python.

```python
# Create sample XML table for performance demos
cursor.execute("""
    CREATE TABLE #XMLPerf (
        OrderID INT,
        OrderXML XML
    )
""")
cursor.execute("""
    INSERT INTO #XMLPerf (OrderID, OrderXML) VALUES
    (1, '<Order><Customer Name="Alice"/><Item ProductID="1" Qty="2" Price="29.99"/></Order>'),
    (2, '<Order><Customer Name="Bob"/><Item ProductID="3" Qty="1" Price="49.99"/></Order>')
""")

# Anti-pattern: Downloads entire XML per row
cursor.execute("SELECT * FROM #XMLPerf")

# Better: Extract only the values you need on the server
cursor.execute("""
    SELECT OrderID,
           OrderXML.value('(/Order/Customer/@Name)[1]', 'NVARCHAR(100)') AS Customer
    FROM #XMLPerf
""")
```

### Handle NULL XML

When querying XML data, use `COALESCE()` to provide default values when XML methods return NULL.

```python
cursor.execute("""
    SELECT OrderID,
           COALESCE(OrderXML.value('(/Order/@Total)[1]', 'DECIMAL(10,2)'), 0) AS Total
    FROM #XMLPerf
""")
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Use JSON data with mssql-python](json-data.md)
- [XML data (SQL Server)](../../../relational-databases/xml/xml-data-sql-server.md)
