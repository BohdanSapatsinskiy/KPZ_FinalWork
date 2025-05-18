# MultiParser

**MultiParser** — це десктопний застосунок для зчитування вмісту вебсторінок на основі введених користувачем CSS-селекторів. Підтримується парсинг текстових елементів і зображень з можливістю збереження в CSV-файл.

## Основні можливості

- Парсинг одного або декількох URL-адрес
- Автоматичне збереження зображень із сайту
- Збереження результатів у CSV
- Перевірка дублювання імен файлів
- Фільтрація посилань за доменом

---

## Programming Principles

1. **Single Responsibility Principle (SRP)**  
   Кожен клас виконує одну чітку функцію:  
   - [`Form1.cs`](./Form1.cs) — обробка UI та подій  
   - [`PageParser.cs`](./PageParser.cs) — логіка парсингу  
   - [`HtmlCleaner.cs`](./HtmlCleaner.cs) — очищення HTML для селекторів

2. **Open/Closed Principle**  
   Нові типи блоків (`TextElementBlock`, `ImageElementBlock`) можна додавати без зміни існуючого коду завдяки успадкуванню від [`ElementBlock`](./ElementBlock.cs).

3. **Dependency Inversion Principle**  
   [`PageParser`](./PageParser.cs) отримує залежності (`driver`, `elements`, `logAction`) через конструктор, що спрощує тестування і розширення.

4. **Don't Repeat Yourself (DRY)**  
   Метод [`RunParsing`](./Form1.cs#L248-L270) дозволяє не дублювати логіку при обробці одного чи кількох URL.

5. **Fail Fast**  
   Більшість методів перевіряють вхідні дані на коректність на початку і завершують виконання одразу при виявленні помилки.

---

## Design Patterns

1. **Strategy Pattern**  
   Застосовується у вигляді `Action<string> logAction`, яка передається в [`PageParser`](./PageParser.cs) ззовні (`Form1.cs`). Це дозволяє задавати стратегію логування, не змінюючи сам парсер.  

2. **Template Method Pattern**  
   Базовий клас [`ElementBlock`](./ElementBlock.cs) задає загальну структуру UI-блоку, а похідні класи (`TextElementBlock`, `ImageElementBlock`) реалізують специфічні частини.  

3. **Factory Construction**  
   У методах [`buttonAddTextElem_Click`](./Form1.cs#L15-L20) та [`buttonAddImg_Click`](./Form1.cs#L22-L27) створюються об'єкти відповідного типу залежно від дії користувача. Хоча це не класична реалізація **Factory Method**, така логіка є прикладом **ручної фабрикації об'єктів**, яка в майбутньому може бути масштабована до повноцінного фабричного методу.  

---

## Refactoring Techniques

1. **Extract Method**  
   Метод [`RunParsing`](./Form1.cs#L248-L270) винесений із `buttonCopyFromUrl_Click` та `buttonCopyMany_Click`, щоб уникнути дублювання.

2. **Replace Magic Numbers with Constants**  
   Замість жорстко заданих значень використовується змінна `maxAllowedImages`, `iteration > 50`, тощо.

3. **Encapsulate Field**  
   Властивість `CsvSaveDirectory` в `PageParser` забезпечує контроль над шляхом збереження CSV.

4. **Rename for Clarity**  
   Імена на кшталт `ParseImageElement`, `InputSavePath`, `IsOne` точно передають призначення змінних/методів.

5. **Split Variable**  
   Змінні `baseFileName`, `indexedFileName` тощо — розділені для читабельності замість конкатенацій в одному виразі.
