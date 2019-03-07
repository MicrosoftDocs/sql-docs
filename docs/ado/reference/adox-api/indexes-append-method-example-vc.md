---
title: "Indexes Append Method Example (VC++) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "Indexes Append method [ADOX], VC++ example"
ms.assetid: 33c559c4-4db7-4850-9309-2743a7ae5521
author: MightyPen
ms.author: genemi
manager: craigg
---
# Indexes Append Method Example (VC++)
The following code demonstrates how to create a new index. The index is on two columns in the table.  
  
```  
// BeginCreateIndexCpp.cpp  
// compile with: /EHsc  
#import "msadox.dll" no_namespace  
  
#include "iostream"  
using namespace std;  
  
// Function declarations  
inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};  
  
int main() {  
   if ( FAILED(::CoInitialize(NULL)) )  
      return -1;  
  
   HRESULT hr = S_OK;  
  
   // Define and initialize ADOX object pointers.  These are in ADODB namespace.  
   _TablePtr m_pTable = NULL;  
   _IndexPtr m_pIndex = NULL;  
   _CatalogPtr m_pCatalog = NULL;  
  
   // Define other variables  
   _bstr_t strcnn("Provider='Microsoft.JET.OLEDB.4.0';Data source = 'c:\\Northwind.mdb';");  
   try {  
      TESTHR(hr = m_pTable.CreateInstance(__uuidof(Table)));  
      TESTHR(hr = m_pIndex.CreateInstance(__uuidof(Index)));  
      TESTHR(hr = m_pCatalog.CreateInstance(__uuidof (Catalog)));  
  
      // Open the catalog.  
      m_pCatalog->PutActiveConnection(strcnn);  
  
      // Define the table and append it to the catalog.  
      m_pTable->Name = "MyTable";  
      m_pTable->Columns->Append("Column1", adInteger,0);  
      m_pTable->Columns->Append("Column2", adInteger,0);  
      m_pTable->Columns->Append("Column3", adVarWChar,50);  
      m_pCatalog->Tables->Append(_variant_t((IDispatch *)m_pTable));  
      printf("Table 'MyTable' is appended.\n");  
  
      // Define a multi-column index.  
      m_pIndex->Name = "multicolidx";  
      m_pIndex->Columns->Append("Column1", adInteger,0);  
      m_pIndex->Columns->Append("Column2", adInteger,0);  
  
      // Append the index to the table.  
      m_pTable->Indexes->Append(_variant_t((IDispatch *)m_pIndex));  
      printf("Index 'multicolidx' is appended.\n");  
  
      // Delete the table.  
      m_pCatalog->Tables->Delete("MyTable");  
      printf("Table 'MyTable' is deleted.\n");  
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      _bstr_t bstrSource(e.Source());  
      _bstr_t bstrDescription(e.Description());  
  
      printf("\n\tSource :  %s \n\tdescription : %s \n ", (LPCSTR)bstrSource, (LPCSTR)bstrDescription);  
   }  
   catch(...) {  
      cout << "Error occured in CreateIndexX...." << endl;  
   }  
   ::CoUninitialize();  
}  
```
