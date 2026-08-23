---
title: "Atomization (XQuery)"
description: Learn about the process of atomization in XQuery in which the typed values of an item are extracted.
author: rothja
ms.author: jroth
ms.reviewer: randolphwest
ms.date: 09/26/2025
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "XQuery, atomization"
  - "atomization [XQuery]"
dev_langs:
  - "XML"
---
# Atomization (XQuery)

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

Atomization is the process of extracting the typed value of an item. This process is implied under certain circumstances. Some of the XQuery operators, such as arithmetic and comparison operators, depend on this process. For example, when you apply arithmetic operators directly to nodes, the typed value of a node is first retrieved by implicitly invoking the [data function](data-accessor-functions-data-xquery.md). This passes the atomic value as an operand to the arithmetic operator.

For example, the following query returns the total of the `LaborHours` attributes. In this case, `data()` is implicitly applied to the attribute nodes.

```sql
DECLARE @x AS XML;

SET @x = '<ROOT><Location LID="1" SetupTime="1.1" LaborHours="3.3" />
<Location LID="2" SetupTime="1.0" LaborHours="5" />
<Location LID="3" SetupTime="2.1" LaborHours="4" />
</ROOT>';
-- data() implicitly applied to the attribute node sequence.

SELECT @x.query('sum(/ROOT/Location/@LaborHours)');
```

Although not required, you can also explicitly specify the `data()` function:

```sql
SELECT @x.query('sum(data(ROOT/Location/@LaborHours))');
```

Another example of implicit atomization is when you use arithmetic operators. The `+` operator requires atomic values, and `data()` is implicitly applied to retrieve the atomic value of the `LaborHours` attribute. The query is specified against the Instructions column of the **xml** type in the ProductModel table. The following query returns the `LaborHours` attribute three times. In the query, consider:

- In constructing the `OriginalLaborHours` attribute, atomization is implicitly applied to the singleton sequence returned by `$WC/@LaborHours`. The typed value of the `LaborHours` attribute is assigned to `OriginalLaborHours`.

- In constructing the `UpdatedLaborHoursV1` attribute, the arithmetic operator requires atomic values. Therefore, `data()` is implicitly applied to the `LaborHours` attribute returned by `$WC/@LaborHours`. The atomic value 1 is then added to it. The construction of attribute `UpdatedLaborHoursV2` shows the explicit application of `data()`, but isn't required.

```sql
SELECT Instructions.query('
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";
for $WC in /AWMI:root/AWMI:Location[1]
        return
            <WC OriginalLaborHours = "{ $WC/@LaborHours }"
                UpdatedLaborHoursV1 = "{ $WC/@LaborHours + 1 }"
                UpdatedLaborHoursV2 = "{ data($WC/@LaborHours) + 1 }" >
            </WC>') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 7;
```

Here's the result:

```sql
<WC OriginalLaborHours="2.5"
    UpdatedLaborHoursV1="3.5"
    UpdatedLaborHoursV2="3.5" />
```

The atomization results in an instance of a simple type, an empty set, or a static type error.

Atomization also occurs in comparison expression parameters passed to functions, values returned by functions, `cast()` expressions, and ordering expressions passed in the order by clause.

## Related content

- [XQuery basics](xquery-basics.md)
- [Comparison Expressions (XQuery)](comparison-expressions-xquery.md)
- [XQuery Functions against the xml Data Type](xquery-functions-against-the-xml-data-type.md)
