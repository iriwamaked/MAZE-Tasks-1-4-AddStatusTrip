# MAZE-Tasks-1-4-+Добавлить StatusTrip (здоровье, количество шагов, время от начала игры)
1.	Реализовать перемещение персонажа по лабиринту стрелками. Персонаж не должен проходить сквозь стены.
   Если персонаж  дошёл до выхода из лабиринта в нижнем правом углу - игра заканчивается победой (вывести диалог с сообщением "победа - найден выход").

2.	Реализовать подсчёт собранных медалей, количество медалей в наличии выводить в заголовок окна (начиная с нуля медалей).
   Если все медали лабиринта собраны - игра заканчивается победой (вывести диалог с сообщением "победа - медали собраны").

3.	Реализовать систему "здоровье персонажа": изначально здоровье на уровне 100% (выводить текущее состояние здоровья в заголовок окна).
   Пересечение с каждым врагом отнимает от 20 до 25% здоровья, при этом враг исчезает. Добавить новый тип объектов лабиринта - "лекарство", который при сборе поправляет здоровье на 5%. Здоровье персонажа не может быть более 100%, то есть, если здоровье уже на максимуме, то лекарство нельзя подобрать. Если здоровье закончилось (упало до 0%) - игра заканчивается поражением (вывести диалог с сообщением "поражение - закончилось здоровье").

4.	Реализовать систему «энергия персонажа»: изначально энергии 500 единиц.
   Каждое перемещение персонажа тратит одну единицу энергии (выводить текущее состояние энергии в заголовок окна).
  	Добавить новый тип объектов лабиринта - "чашка кофе", которая при сборе повышает запас энергии на 25 единиц. Чашку кофе нельзя выпить, если персонаж совершил менее 10 перемещений с момента принятия лекарства. Других ограничений на максимальный уровень запаса энергии нет. Если энергия закончилась - игра заканчивается поражением (вывести диалог с сообщением "поражение – закончилась энергия").
