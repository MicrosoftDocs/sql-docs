---
title: Develop using REST APIs
description: The REST API provides programmatic access to the objects in a SQL Server 2017 Reporting Services report server catalog.
author: maggiesMSFT
ms.author: maggies
ms.service: reporting-services
ms.subservice: developer
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/12/2018
---

# Develop with the REST APIs for Reporting Services

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

Microsoft SQL Server 2017 Reporting Services support Representational State Transfer (REST) APIs. The REST APIs are service endpoints that support a set of HTTP operations (methods), which provide create, retrieve, update, or delete access for resources within a report server.

The REST API provides programmatic access to the objects in a SQL Server 2017 Reporting Services report server catalog. Examples of objects are folders, reports, KPIs, data sources, datasets, refresh plans, subscriptions, and more. Using the REST API, you can, for example, navigate the folder hierarchy, discover the contents of a folder, or download a report definition. You can also create, update, and delete objects. Examples of working with objects are upload a report, execute a refresh plan, delete a folder, and so on.

[!INCLUDE [GDPR-related guidance](../../includes/gdpr-hybrid-note.md)]

## Components of a REST API request/response

A REST API request/response pair can be separated into five components:

* The **request URI**, which consists of: `{URI-scheme} :// {URI-host} / {resource-path} ? {query-string}`. Although the request URI is included in the request message header, we call it out separately here because most languages or frameworks require you to pass it separately from the request message.

    * URI scheme: Indicates the protocol used to transmit the request. For example, `http` or `https`.
    * URI host: Specifies the domain name or IP address of the server where the REST service endpoint is hosted, such as `myserver.contoso.com`.
    * Resource path: Specifies the resource or resource collection, which may include multiple segments used by the service in determining the selection of those resources. For example: `CatalogItems(01234567-89ab-cdef-0123-456789abcdef)/Properties` can be used to get the specified properties for the CatalogItem.
    * Query string (optional): Provides additional simple parameters, such as the API version or resource selection criteria.

* HTTP request message header fields:

    * A required [HTTP method](http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html) (also known as an operation or verb), which tells the service what type of operation you are requesting. Reporting Services REST APIs support DELETE, GET, HEAD, PUT, POST, and PATCH methods.
    * Optional additional header fields, as required by the specified URI and HTTP method.

* Optional HTTP **request message body** fields, to support the URI and HTTP operation. For example, POST operations contain MIME-encoded objects that are passed as complex parameters. For POST or PUT operations, the MIME-encoding type for the body should be specified in the `Content-type` request header as well. Some services require you to use a specific MIME type, such as `application/json`.

* HTTP **response message header** fields:

    * An [HTTP status code](http://www.w3.org/Protocols/HTTP/HTRESP.html), ranging from 2xx success codes to 4xx or 5xx error codes. Alternatively, a service-defined status code may be returned, as indicated in the API documentation.
    * Optional additional header fields, as required to support the request's response, such as a `Content-type` response header.

* Optional HTTP **response message body** fields:

    * MIME-encoded response objects are returned in the HTTP response body, such as a response from a GET method that is returning data. Typically, these objects are returned in a structured format such as JSON or XML, as indicated by the `Content-type` response header.

## API documentation

A modern REST API calls for modern API documentation. The REST API is built on the OpenAPI specification (also called the swagger specification) and documentation is available on [SwaggerHub](https://app.swaggerhub.com/api/microsoft-rs/SSRS/2.0). Beyond documenting the API, SwaggerHub helps generate a client library in the language of choice - JavaScript, TypeScript, C#, Java, Python, Ruby, and more.

## Testing API calls

A tool for testing HTTP request/response messages is [Fiddler](https://www.telerik.com/fiddler). Fiddler is a free web debugging proxy that can intercept your REST requests, making it easy to diagnose the HTTP request/ response messages.

## Next steps

Review the available APIs over on [SwaggerHub](https://app.swaggerhub.com/api/microsoft-rs/SSRS/2.0).

Samples are available on [GitHub](https://github.com/Microsoft/Reporting-Services). The sample includes an HTML5 app built on TypeScript, React, and webpack along with a PowerShell example.

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
