public static void executeStatement(Connection con) {
   try {
      String SQL = "SELECT LastName, FirstName FROM Person.Contact ORDER BY LastName";
      Statement stmt = con.createStatement();
      ResultSet rs = stmt.executeQuery(SQL);

      while (rs.next()) {
         System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
      }
      rs.close();
      stmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}