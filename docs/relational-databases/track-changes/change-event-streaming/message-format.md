---
title: "JSON Message format - change event streaming"
description: "Describes the message format for change event streaming"
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma
ms.date: 07/29/2026
ms.service: sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# JSON message format - change event streaming
[!INCLUDE [sql25-sqldb-sqlmi-sqldbfabric](../../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

This article describes the JSON format of a CloudEvents message that streams to Azure Event Hubs or Fabric Eventstream when using the [change event streaming (CES)](overview.md) feature in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

[!INCLUDE [change-event-streaming-preview](../../../includes/change-event-streaming-preview.md)]

## Overview

Change event streaming emits events that follow the [CloudEvents](https://github.com/cloudevents/spec) specification, so you can easily integrate them with event-driven systems. All CES CloudEvents contain 11 attributes (fields). You can configure CES to serialize CloudEvents as JSON (native) or as Avro binary. The following sections of this article describe the message format in detail, including CES CloudEvent attributes and serialization.

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

## Related specifications and resources

When applicable, the descriptions in this section come from the [CloudEvent specification](https://github.com/cloudevents/spec/blob/v1.0.2/cloudevents/spec.md), which includes more details.  

## Attributes

- **`specversion`**:
  - Data type: String
  - Required CloudEvent attribute
  - The version of the CloudEvents specification that the event uses. This version enables the interpretation of the context.

- **`type`**
  - Data type: String
  - Required CloudEvent attribute
  - Contains a value that describes the type of event related to the originating occurrence. The format of this value is defined by the producer and might include information such as the version of the type. For more information, see [Versioning of CloudEvents](https://github.com/cloudevents/spec/blob/v1.0.2/cloudevents/primer.md).
  - For change event streaming events, the type is currently: `com.microsoft.SQL.CES.DML.V{n}`, where `{n}` indicates the version of Microsoft change event streaming DML event schema.
    - The current latest schema version is 1.

- **`source`**
  - Data type: String
  - Required CloudEvent attribute
  - Identifies the context in which an event happened. The combination of source and ID must be unique for each event. Currently, this field is always sent as `\/` in events streamed from SQL.

- **`id`**
  - Data type: String
  - Required CloudEvent attribute
  - Identifies the event. Producers must ensure that the combination of source and ID is unique for each distinct event. If a duplicate event is resent (for example, due to a network error) it could have the same ID. Consumers might assume that events with identical source and ID are duplicates.

- **`logicalid`**
  - Data type: String
  - Extension attribute
  - Shared logical IDs identify split messages (due to Event Hubs message size restrictions).

- **`time`**
  - Data type: Timestamp
  - Optional CloudEvent attribute
  - UTC timestamp of when the commit happened within a SQL transaction that originally triggers a streamed event.

- **datacontenttype**
  - Data type: String
  - Optional CloudEvent attribute
  - Content type of data value. This attribute enables data to carry any type of content, whereby format and encoding might differ from that of the chosen event format. For example, an event rendered using the JSON envelope format might carry an XML payload in the data, and the consumer is informed by this attribute being set to "application/xml". The rules for how data content is rendered for different `datacontenttype` values are defined in the event format specifications.

- **`operation`**
  - Data type: String
  - Extension attribute
  - Represents the type of SQL operation that happened:
    -  INS for inserts
    -  UPD for updates
    -  DEL for deletes

- **`segmentindex`**
  - Data type: Integer
  - Extension attribute
  - Segment index, which denotes the position of the message within the logical message chunks. The segment index provides information about where the message stands in the sequence of logical message fragments. This field is always present. Use `logicalid`, `segmentindex`, and `finalsegment` fields to sort incoming events that represent a large SQL payload split according to the configured `max_message_size_kb` value.

- **`finalsegment`**
  - Data type: Boolean
  - Extension attribute
  - Indicates whether this segment is the final segment of the sequence. This field is always present and helps to identify whether a SQL event was split into subevents according to the configured `max_message_size_kb` value.

- **`data`**
  - Data type: String
  - Optional CloudEvent attribute
  - Domain specific event data. For CES, data is string that can be parsed as JSON. This JSON describes how data changed. Format of the data attribute is at [Data attribute format](#data-attribute-format).

> [!NOTE]
> Message splitting is separate from column-value truncation. Before CES serializes the `data` attribute, it truncates each streamed column value larger than 1 MB to 1 MB. CES then splits the formed event into message chunks as needed according to `max_message_size_kb`.

## Examples

### JSON message example - insert

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "d43f09a6-d13b-4902-86d4-17bdb5edb872",
  "logicalid": "9c8d4ad2-bf54-4f10-a96f-038af496997f:0000002C00000300017C:00000000000000000001",
  "time": "2025-03-14T16:45:20.650Z",
  "datacontenttype": "application\/json",
  "operation": "INS",
  "splitindex": 0,
  "splittotalcnt": 0,
  "data": "{\n  \"eventsource\": {\n    \"db\": \"db1\",\n    \"schema\": \"dbo\",\n    \"tbl\": \"Purchases\",\n    \"cols\": [\n      {\n        \"name\": \"purchase_id\",\n        \"type\": \"int\",\n        \"index\": 0\n      },\n      {\n        \"name\": \"customer_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 1\n      },\n      {\n        \"name\": \"product_id\",\n        \"type\": \"int\",\n        \"index\": 2\n      },\n      {\n        \"name\": \"product_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 3\n      },\n      {\n        \"name\": \"price_per_item\",\n        \"type\": \"int\",\n        \"index\": 4\n      },\n      {\n        \"name\": \"quantity\",\n        \"type\": \"int\",\n        \"index\": 5\n      },\n      {\n        \"name\": \"purchase_date\",\n        \"type\": \"datetime\",\n        \"index\": 6\n      },\n      {\n        \"name\": \"payment_method\",\n        \"type\": \"varchar(50)\",\n        \"index\": 7\n      }\n    ],\n    \"pkkey\": [\n      {\n        \"columnname\": \"purchase_id\",\n        \"value\": \"105\"\n      }\n    ]\n  },\n  \"eventrow\": {\n    \"old\": \"{}\",\n    \"current\": \"{\\\"purchase_id\\\": \\\"105\\\", \\\"customer_name\\\": \\\"Anna Doe\\\", \\\"product_id\\\": \\\"101\\\", \\\"product_name\\\": \\\"Game 2077\\\", \\\"price_per_item\\\": \\\"60\\\", \\\"quantity\\\": \\\"1\\\", \\\"purchase_date\\\": \\\"2025-03-14 16:45:01.000\\\", \\\"payment_method\\\": \\\"Credit Card\\\"}\"\n  }\n}"
}
```

### JSON message example - updated

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "c425575f-00bb-45cf-acec-c55fdc7d08cd",
  "logicalid": "9c8d4ad2-bf54-4f10-a96f-038af496997f:0000002C000003500004:00000000000000000001",
  "time": "2025-03-14T16:49:59.567Z",
  "datacontenttype": "application\/json",
  "operation": "UPD",
  "splitindex": 0,
  "splittotalcnt": 0,
  "data": "{\n  \"eventsource\": {\n    \"db\": \"db1\",\n    \"schema\": \"dbo\",\n    \"tbl\": \"Purchases\",\n    \"cols\": [\n      {\n        \"name\": \"purchase_id\",\n        \"type\": \"int\",\n        \"index\": 0\n      },\n      {\n        \"name\": \"customer_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 1\n      },\n      {\n        \"name\": \"product_id\",\n        \"type\": \"int\",\n        \"index\": 2\n      },\n      {\n        \"name\": \"product_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 3\n      },\n      {\n        \"name\": \"price_per_item\",\n        \"type\": \"int\",\n        \"index\": 4\n      },\n      {\n        \"name\": \"quantity\",\n        \"type\": \"int\",\n        \"index\": 5\n      },\n      {\n        \"name\": \"purchase_date\",\n        \"type\": \"datetime\",\n        \"index\": 6\n      },\n      {\n        \"name\": \"payment_method\",\n        \"type\": \"varchar(50)\",\n        \"index\": 7\n      }\n    ],\n    \"pkkey\": [\n      {\n        \"columnname\": \"purchase_id\",\n        \"value\": \"105\"\n      }\n    ]\n  },\n  \"eventrow\": {\n    \"old\": \"{}\",\n    \"current\": \"{\\\"purchase_id\\\": \\\"105\\\", \\\"customer_name\\\": \\\"Anna Doe\\\", \\\"product_id\\\": \\\"100\\\", \\\"product_name\\\": \\\"Game 2066\\\", \\\"price_per_item\\\": \\\"50\\\", \\\"quantity\\\": \\\"2\\\", \\\"purchase_date\\\": \\\"2025-03-14 16:45:01.000\\\", \\\"payment_method\\\": \\\"Credit Card\\\"}\"\n  }\n}"
}
```

### JSON message example - delete

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "24fa0c2c-c45d-4abf-9a8d-fba04c29fc86",
  "logicalid": "9c8d4ad2-bf54-4f10-a96f-038af496997f:0000002C000003600019:00000000000000000001",
  "time": "2025-03-14T16:51:39.613Z",
  "datacontenttype": "application\/json",
  "operation": "DEL",
  "splitindex": 0,
  "splittotalcnt": 0,
  "data": "{\n  \"eventsource\": {\n    \"db\": \"db1\",\n    \"schema\": \"dbo\",\n    \"tbl\": \"Purchases\",\n    \"cols\": [\n      {\n        \"name\": \"purchase_id\",\n        \"type\": \"int\",\n        \"index\": 0\n      },\n      {\n        \"name\": \"customer_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 1\n      },\n      {\n        \"name\": \"product_id\",\n        \"type\": \"int\",\n        \"index\": 2\n      },\n      {\n        \"name\": \"product_name\",\n        \"type\": \"varchar(100)\",\n        \"index\": 3\n      },\n      {\n        \"name\": \"price_per_item\",\n        \"type\": \"int\",\n        \"index\": 4\n      },\n      {\n        \"name\": \"quantity\",\n        \"type\": \"int\",\n        \"index\": 5\n      },\n      {\n        \"name\": \"purchase_date\",\n        \"type\": \"datetime\",\n        \"index\": 6\n      },\n      {\n        \"name\": \"payment_method\",\n        \"type\": \"varchar(50)\",\n        \"index\": 7\n      }\n    ],\n    \"pkkey\": [\n      {\n        \"columnname\": \"purchase_id\",\n        \"value\": \"105\"\n      }\n    ]\n  },\n  \"eventrow\": {\n    \"old\": \"{\\\"purchase_id\\\": \\\"105\\\", \\\"customer_name\\\": \\\"Anna Doe\\\", \\\"product_id\\\": \\\"100\\\", \\\"product_name\\\": \\\"Game 2066\\\", \\\"price_per_item\\\": \\\"50\\\", \\\"quantity\\\": \\\"2\\\", \\\"purchase_date\\\": \\\"2025-03-14 16:45:01.000\\\", \\\"payment_method\\\": \\\"Credit Card\\\"}\",\n    \"current\": \"{}\"\n  }\n}"
}
```

## Data attribute format

Data is a JSON object wrapped in string attribute that contains two attributes:

- `eventSource`
- `eventRow` 

```json
"data": "{ "eventsource": {<eventSource>}, "eventrow": {<eventRow>}}"
```

The following sections explain these two attributes in greater detail. 

### eventsource

Describes the metadata about the database and the table where the event occurred: 

- `db`
  - Data type: String
  - Description: The name of the database where the table is located.
  - Example: `cessqldb001`

- `schema`
  - Data type: String
  - Description: The database schema that contains the table.
  - Example: `dbo`

- `tbl`
  - Data type: String
  - Description: The table in which the event occurred. 
  - Example: `Purchases`

- `cols`
  - Data type: Array
  - Description: An array detailing the columns in the table.
    - name (**string**): The name of the column.
    - type (**string**): The data type of the column (**VARCHAR** or **INT**).
    - index (**integer**): The index or position of the column in the table.

- `pkkey`
  - Data type: Array
  - Description: Represents the primary key columns and their values for identifying the specific row.
    - columnname (**string**): The name of column used in the primary key.
    - value (**string**/**int**/etc.): The value for the column used in the primary key, helps to uniquely identify the row. 

### eventrow

Describes row-level changes and compares the old and current values of the fields in the record.

- **old** (object wrapped in string): Represents the values in the row before the event.
  - Each key-value pair consists of:
    - `<column_name>`: (string): The name of the column.
    - `<column_value>`:  (string/int/etc.): The previous value for that column.
- **current** (object wrapped in string): Represents the updated values in the row after the event.
  - Similar to the old object, with each key-value pair structured as:
    - `<column_name>` (string): The name of the column.
    - `<column_value>` (string/int/etc.): The new or current value for that column.

## CES CloudEvent AVRO schema

```json
{
  "type": "record",
  "name": "ChangeEvent",
  "fields": [
    {
      "name": "specversion",
      "type": "string"
    },
    {
      "name": "type",
      "type": "string"
    },
    {
      "name": "source",
      "type": "string"
    },
    {
      "name": "id",
      "type": "string"
    },
    {
      "name": "logicalid",
      "type": "string"
    },
    {
      "name": "time",
      "type": "string"
    },
    {
      "name": "datacontenttype",
      "type": "string"
    },
    {
      "name": "operation",
      "type": "string"
    },
    {
      "name": "segmentindex",
      "type": "int"
    },
    {
      "name": "finalsegment",
      "type": "boolean"
    },
    {
      "name": "data",
      "type": "bytes"
    }
  ]
}
```

### CES data attribute AVRO schema

```json
{
  "name": "Data",
  "type": "record",
  "fields": [
    {
      "name": "eventsource",
      "type": {
        "name": "EventSource",
        "type": "record",
        "fields": [
          {
            "name": "db",
            "type": "string"
          },
          {
            "name": "schema",
            "type": "string"
          },
          {
            "name": "tbl",
            "type": "string"
          },
          {
            "name": "cols",
            "type": {
              "type": "array",
              "items": {
                "name": "Column",
                "type": "record",
                "fields": [
                  {
                    "name": "name",
                    "type": "string"
                  },
                  {
                    "name": "type",
                    "type": "string"
                  },
                  {
                    "name": "index",
                    "type": "int"
                  }
                ]
              }
            }
          },
          {
            "name": "pkkey",
            "type": {
              "type": "array",
              "items": {
                "name": "PkKey",
                "type": "record",
                "fields": [
                  {
                    "name": "columnname",
                    "type": "string"
                  },
                  {
                    "name": "value",
                    "type": "string"
                  }
                ]
              }
            }
          },
          {
            "name": "transaction",
            "type": {
              "name": "Transaction",
              "type": "record",
              "fields": [
                {
                  "name": "commitlsn",
                  "type": "string"
                },
                {
                  "name": "beginlsn",
                  "type": "string"
                },
                {
                  "name": "sequencenumber",
                  "type": "int"
                },
                {
                  "name": "committime",
                  "type": "string"
                }
              ]
            }
          }
        ]
      }
    },
    {
      "name": "eventrow",
      "type": {
        "name": "EventRow",
        "type": "record",
        "fields": [
          {
            "name": "old",
            "type": "string"
          },
          {
            "name": "current",
            "type": "string"
          }
        ]
      }
    }
  ]
}
```


## Related content

- [What is change event streaming?](overview.md)
- [Configure change event streaming](configure.md)
- [Frequently asked CES questions](frequently-asked-questions-faq.yml)
