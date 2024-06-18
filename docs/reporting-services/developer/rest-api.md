---
title: What are REST APIs for Reporting Services?
description: Learn how REST APIs provide programmatic access to the objects in a SQL Server 2017 Reporting Services report server catalog.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/18/2024
ms.service: reporting-services
ms.subservice: developer
ms.topic: overview
ms.custom: updatefrequency5

#customer intent: As a SQL server user, I want to use REST APIs to access resources within a report server so that I can manage objects in a report server catalog.
---

# What are REST APIs for Reporting Services?

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

Microsoft SQL Server 2017 Reporting Services supports Representational State Transfer (REST) APIs. REST APIs are service endpoints that support a set of HTTP operations (methods). These methods provide create, retrieve, update, or delete access for resources within a report server.

The REST API provides programmatic access to the objects in a SQL Server 2017 Reporting Services report server catalog. The following are examples of objects:

- Folders
- Reports
- KPIs
- Data sources
- Datasets
- Refresh plans
- Subscriptions

When you use the REST API, you can navigate the folder hierarchy, discover the contents of a folder, or download a report definition. You can also create, update, and delete objects.

[!INCLUDE [GDPR-related guidance](../../includes/gdpr-hybrid-note.md)]

## Components of a REST API request/response

A REST API request/response pair can be separated into five components:

- The **request URI**:
  - Although the request URI is included in the request message header, most languages or frameworks require you to pass it separately from the request message.
  - Consists of `{URI-scheme} :// {URI-host} / {resource-path} ? {query-string}`.

  |Request URI fragments|Description|
  |---|---|
  |URI scheme|Indicates the protocol used to transmit the request. For example, `http` or `https`.|
  |URI host| Specifies the domain name or IP address of the server where the REST service endpoint is hosted, such as `myserver.contoso.com`.|
  |Resource path| Specifies the resource or resource collection, which might include multiple segments used by the service in determining the selection of those resources. For example, you can use `CatalogItems(01234567-89ab-cdef-0123-456789abcdef)/Properties` to get the specified properties for the `CatalogItem`.|
  |Query string (optional)|Provides more simple parameters, such as the API version or resource selection criteria.|

- HTTP **request message header** fields:
  - A required [HTTP method](http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html), also known as an operation or verb.
  - Tells the service what type of operation you're requesting. Reporting Services REST APIs support DELETE, GET, HEAD, PUT, POST, and PATCH methods.
  - Optional extra header fields, as required by the specified URI and HTTP method.

- Optional HTTP **request message body** fields:
  - Supports the URI and HTTP operation. For example, POST operations contain Multipurpose Internet Mail Extensions (MIME) encoded objects that are passed as complex parameters.
  - For POST or PUT operations, the MIME-encoding type for the body should be specified in the `Content-type` request header as well. Some services require you to use a specific MIME type, such as `application/json`.

- HTTP **response message header** fields:
  - An [HTTP status code](http://www.w3.org/Protocols/HTTP/HTRESP.html), ranging from 2xx success codes to 4xx or 5xx error codes. Alternatively, a service-defined status code might be returned, as indicated in the API documentation.
  - Optional extra header fields, as required to support the request's response, such as a `Content-type` response header.

- Optional HTTP **response message body** fields:
  - MIME-encoded response objects are returned in the HTTP response body, such as a response from a GET method that is returning data. Typically, these objects are returned in a structured format such as JSON or XML, as indicated by the `Content-type` response header.

## API documentation

A modern REST API calls for modern API documentation. The REST API is built on the OpenAPI specification, which is also called the swagger specification. Documentation is available on [Microsoft Learn](/rest/api/power-bi-report/).

## Test API calls

If you need a tool for testing HTTP request/response messages, [Fiddler](https://www.telerik.com/fiddler) is a free web debugging proxy that can intercept your REST requests, making it easy to diagnose the HTTP request/response messages.

## Related content

- [Use the Power BI Report Server REST APIs](/rest/api/power-bi-report/)
- [Reporting-Services samples](https://github.com/Microsoft/Reporting-Services)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
