# VaultManager
Решение тестового задания by Doronin Ivan

# Часть 1
В качестве объекта, который мы храним в Node, используется модель (класс) представляющая данные о клиенте, см. Node.cs, где свойства и класс помечены атрибутами для сериализации и десериализации.

# Часть 2
Сделал для себя и удобства тестирования дополнение к программе, можно создавать рандомный Node.

# Часть 3 (Про скорость)
Из библиотечных структур использовался Array.Copy(), я решил не писать свой, так как особо на скорость выполнения это не повлияет, даже на больших данных.
В качестве реализации я хранил Nodes в классе Vault в виде массива, также хранится текущий индекс для быстрого добавления в Vault, если массив заполнен, то массив пересоздается (копируются элементы из старого массива) и его размер увеличивается вдвое. Начальный размер массива = 4. Примерно с такой же идеей работает List<T>.

# Часть 4
Не обрабатывалась возможность читать файлы из подкаталогов, но в тз об этом ничего не упоминалось.

# Часть 5 (Ошибки)
