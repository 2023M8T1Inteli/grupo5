'use client';
import { useState } from 'react';
import { format, startOfMonth, endOfMonth, startOfWeek, endOfWeek, addDays, subMonths, addMonths, isSameMonth, isSameDay } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';
import LeftArrow from '@/public/left.svg';
import RightArrow from '@/public/right.svg';
import Image from 'next/image';

interface CalendarProps {
	events: { [key: string]: any[] };
	onDateSelect: (date: Date) => void; // callback prop
  }
  

const Calendar: React.FC<CalendarProps> = ({ events, onDateSelect }) => {
  const [currentDate, setCurrentDate] = useState<Date>(new Date());
  const [selectedDate, setSelectedDate] = useState<Date>(new Date());

  const renderHeader = () => {
    const dateFormat = "MMMM";
    return (
      <div className="flex justify-center items-center p-2 gap-5">
        <button
          onClick={() => setCurrentDate(subMonths(currentDate, 1))}
          className="p-2"
        >
          <Image src={LeftArrow} alt="Left Arrow" />
        </button>
        <span className="text-center text-4xl font-medium">
          {format(currentDate, dateFormat, { locale: ptBR }).charAt(0).toUpperCase() + format(currentDate, dateFormat, { locale: ptBR }).slice(1)}
        </span>
        <button
          onClick={() => setCurrentDate(addMonths(currentDate, 1))}
          className="p-2"
        >
          <Image src={RightArrow} alt="Right Arrow" />
        </button>
      </div>
    );
  };

  const renderDaysOfWeek = () => {
    const dateFormat = "EEEE";
    const days = [];

    let startDate = startOfWeek(currentDate, { locale: ptBR });

    for (let i = 0; i < 7; i++) {
      days.push(
        <div className="text-center text-xl text-[#989898] font-medium" key={i}>
          {format(addDays(startDate, i), dateFormat, { locale: ptBR }).charAt(0).toUpperCase() + format(addDays(startDate, i), dateFormat, { locale: ptBR }).substring(1,3)}
        </div>
      );
    }

    return <div className="grid grid-cols-7 border-b-2 border-[#e7e7e7] pb-4">{days}</div>;
  };

  const renderCells = () => {
    const monthStart = startOfMonth(currentDate);
    const monthEnd = endOfMonth(monthStart);
    const startDate = startOfWeek(monthStart, { locale: ptBR });
    const endDate = endOfWeek(monthEnd, { locale: ptBR });

    const dateFormat = "d";
    const rows = [];

    let days = [];
    let day = startDate;
    let formattedDate = "";

    while (day <= endDate) {
		for (let i = 0; i < 7; i++) {
		  formattedDate = format(day, dateFormat);
		  const cloneDay = day;
		  const dayIsSelected = isSameDay(day, selectedDate);
		  const dayIsInCurrentMonth = isSameMonth(day, monthStart);
  
		  days.push(
			<div
			  className={`text-center px-2 py-4 relative font-medium text-2xl cursor-pointer ${
				!dayIsInCurrentMonth
				  ? 'text-gray-400 opacity-40'
				  : dayIsSelected
				  ? 'text-white'
				  : 'text-black'
			  }`}
			  key={day.toString()}
			  onClick={() => { 
				setSelectedDate(cloneDay)
				onDateSelect(cloneDay)
				
				}}
			>
			  <p
				className={`mx-auto w-10 h-10 flex items-center justify-center rounded-full ${
				  dayIsSelected ? 'bg-indigo-700' : ''
				}`}
			  >
				{formattedDate}
			  </p>
			  {events[format(cloneDay, 'yyyy-MM-dd')] && (
				<span className="absolute top-[1rem] right-[1rem] h-2 w-2 bg-red-500 rounded-full" />
			  )}
			</div>
		  );
		  day = addDays(day, 1);
		}
		rows.push(
		  <div className="grid grid-cols-7" key={day.toString()}>
			{days}
		  </div>
		);
		days = [];
	  }
	  return <div className="flex flex-col gap-2">{rows}</div>;
  };

  return (
    <div className='flex flex-col gap-10'>
      {renderHeader()}
      {renderDaysOfWeek()}
      {renderCells()}
    </div>
  );
};

export default Calendar;
