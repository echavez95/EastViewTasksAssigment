export const getWeekDays = () => {
    let weekDays = [{
            DayNumber: 0,
            DayName: "Domingo"
        },
        {
            DayNumber: 1,
            DayName: "Lunes"
        },
        {
            DayNumber: 2,
            DayName: "Martes"
        },
        {
            DayNumber: 3,
            DayName: "Miércoles"
        },
        {
            DayNumber: 4,
            DayName: "Jueves"
        },
        {
            DayNumber: 5,
            DayName: "Viernes"
        },
        {
            DayNumber: 6,
            DayName: "Sábado"
        },
    ]
    return weekDays;
}
export const getWeekDayName = (weekDay) => {
    const day = getWeekDays().find(e=> e.DayNumber === weekDay);
    return day.DayName
}