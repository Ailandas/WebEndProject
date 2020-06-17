<h1>WebEndProject</h1>
**Table of contents:**
1. Installation instructions
2. Operating instructions
3. Configuration
4. Copyright and licensing information
5. Contact information for the distributor or programmer
6. Known bugs
7. Troubleshooting
8. Credits and acknowledgments
9. A changelog (usually for programmers)
10. A news section (usually for users)

1. Būtina įsidiegti duomenų bazę su duomenų modeliu:
Pirma lentelė: „Categories“ ID, Category, Word, DayTime.
Antra lentelė „DayTime“ ID, Name, After, upTo.
Taip pat dėl testų būtina užpildyti pirmą lentelę tokiais duomenimis: 

Category | Word
TestUpdate22	|Test update
Test | Test

2. Administratorių metodai pridėti, trinti, atnaujinti informaciją susijusią su kategorijomis duomenų bazėje: 
  2.1 POST /api/quotedictionary Metodas, iterpiantis nauja irasa i duomenu baze (duomenys iš body JSON formatu)
  2.2 PUT /api/quotedictionary/categories Metodas skirtas atnaujinti kategorijos pavadinima (duomenys iš body JSON formatu)
  2.3 DELETE /api/quotedictionary/categories/{category} (Trinamos kategorijos pavadinimas prirašomas link'e, kur {category})
  2.4 DELETE /api/quotedictionary/words/{word} (Trinamo žodžio pavadinimas prirašomas link'e, kur {word})
  2.5 PUT /api/quotedictionary/words (duomenys iš body JSON formatu)

3. Web.config failo turinys:
<configuration>
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
  <system.web>
    <compilation debug="true" targetFramework="4.7.2" />
    <httpRuntime targetFramework="4.7.2" />
  </system.web>
  <system.webServer>
    <handlers>
      <remove name="ExtensionlessUrlHandler-Integrated-4.0" />
      <remove name="OPTIONSVerbHandler" />
      <remove name="TRACEVerbHandler" />
      <add name="ExtensionlessUrlHandler-Integrated-4.0" path="*." verb="*" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
    </handlers>
  </system.webServer>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="Antlr3.Runtime" publicKeyToken="eb42632606e9261f" />
        <bindingRedirect oldVersion="0.0.0.0-3.5.0.2" newVersion="3.5.0.2" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" culture="neutral" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="0.0.0.0-12.0.0.0" newVersion="12.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Optimization" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-1.1.0.0" newVersion="1.1.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="WebGrease" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-1.6.5135.21930" newVersion="1.6.5135.21930" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Helpers" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.WebPages" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-5.2.7.0" newVersion="5.2.7.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Http" publicKeyToken="31bf3856ad364e35" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-5.2.7.0" newVersion="5.2.7.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Net.Http.Formatting" publicKeyToken="31bf3856ad364e35" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-5.2.7.0" newVersion="5.2.7.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
  <system.codedom>
    <compilers>
      <compiler language="c#;cs;csharp" extension=".cs" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" warningLevel="4" compilerOptions="/langversion:default /nowarn:1659;1699;1701" />
      <compiler language="vb;vbs;visualbasic;vbscript" extension=".vb" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" warningLevel="4" compilerOptions="/langversion:default /nowarn:41008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+" />
    </compilers>
  </system.codedom>
</configuration>

4. Copyringhts: studentai Ailandas Eidintas- @Ailandas ir Ernestas Baniulis- @makakalaka PI18A Viko.
5. Contact information: ailandas.eidintas@stud.viko.lt or @Ailandas & ernestas.baniulis@stud.viko.lt or @makakalaka
6. Knows bugs: Testuojant keli metodai vienu metu kreipiasi į duomenų bazę todėl gali rodyti klaidą pavadinimu „Database locked“  todėl rekomenduojama tokius metodus testuoti po vieną. 
7. Troubleshooting: susidūrus su problema rekomenduojama paleisti visus testus ir žiūrėti ties kuriuo, sustojo. Po to, atsidarius individualų testą, bus galima matyti ties kuriuo testo case'u (metodu) sustojo. Šitokiu būdu bus galima susiaurinti iš kur kilo problema.
8. Credits: kolektyvui studentų.
9. Changelog significant commits: 
 - [x] 9.1 Initial commit
 - [x] 9.2 Komentaras 
 - [x] 9.3 QuotesAPI 
 - [x] 9.4 Dictionary API
 - [x] 9.5 json->object
 - [x] 9.6 Translator API
 - [x] 9.7 Pridėta duomazė ir metodas skirtas patalpinti į ją
 - [x] 9.8 Caching, Hateoas
 - [x] 9.9 POST body
 - [x] 9.10 HATEOAS linkai (from obj)
 - [x] 9.11 Swagger UI
 - [x] 9.12 Fixed: caching category/{word}
 - [x] 9.13 Put methods
 - [x] 9.14 First tests working.
 - [x] 9.15 SpecFlow testai (post, get)
 - [x] 9.16 Tests All
 - [x] 9.17 Testu tikrinimas
 - [x] 9.18 Išverčiamas išrinktas žodis
 - [x] 9.19 Added Readme.md

10. News: latest updates you can see at https://trello.com/b/fy13BQ2B/webservice-rest-end-project
