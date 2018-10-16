---
title: "Create Method Example (VC++) | Microsoft Docs"
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
  - "Create method [ADOX], VC++ example"
ms.assetid: 57fcb0eb-5d40-4ad4-996d-380732de8a3d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Create Method Example (VC++)
The following code shows how to create a new Microsoft Jet database with the [Create](../../../ado/reference/adox-api/create-method-adox.md) method.  
  
```  
// BeginCreateDatabaseCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" rename("EOF", "EndOfFile")  
#import "msadox.dll" no_namespace  
  
#define TESTHR(x) if FAILED(x) _com_issue_error(x);  
  
#include "iostream"  
using namespace std;  
  
// Function declarations  
void CreateDatabaseX(void);  
  
int main() {  
   HRESULT hr = S_OK;  
  
   hr = ::CoInitialize(NULL);  
   if (SUCCEEDED(hr)) {  
      CreateDatabaseX();  
      ::CoUninitialize();  
   }  
}  
  
// Create a new Jet database with the Create method  
void CreateDatabaseX() {  
   HRESULT hr = S_OK;  
  
   // Define ADOX object pointers, initialize pointers. These are in ADODB namespace.  
   _CatalogPtr m_pCatalog = NULL;  
  
   // Set ActiveConnection of Catalog to this string  
   _bstr_t strcnn("Provider='Microsoft.JET.OLEDB.4.0';Data source = c:\\new.mdb");  
   try {  
      TESTHR(hr = m_pCatalog.CreateInstance(__uuidof (Catalog)));  
      m_pCatalog->Create(strcnn);  
      printf("Database 'c:\\new.mdb' is created.\n");  
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      _bstr_t bstrSource(e.Source());  
      _bstr_t bstrDescription(e.Description());  
  
      printf("\n\tSource :  %s \n\tdescription : %s \n ", (LPCSTR)bstrSource, (LPCSTR)bstrDescription);  
   }  
   catch(...) {  
      cout << "Error occured in CreateDatabaseX...." << endl;  
   }  
}  
```  
  
## See Also  
 [Create Method (ADOX)](../../../ado/reference/adox-api/create-method-adox.md)
