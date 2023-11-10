'use client'
import BoardQuadrant from "@/app/components/BoardQuadrant";
import ButtonMin from "@/app/components/ButtonMin";
import Play from '@/public/play_white.svg'
import Trash from '@/public/trash.svg'
import Board1 from '@/public/board-1.svg'
import Board2 from '@/public/board-2.svg'
import Board3 from '@/public/board-3.svg'
import Board4 from '@/public/board-4.svg'
import Board5 from '@/public/board-5.svg'
import Board6 from '@/public/board-6.svg'
import Board7 from '@/public/board-7.svg'
import Board8 from '@/public/board-8.svg'
import Board9 from '@/public/board-9.svg'
import Board10 from '@/public/board-10.svg'
import Board11 from '@/public/board-11.svg'
import Board12 from '@/public/board-12.svg'
import Board13 from '@/public/board-13.svg'
import Board14 from '@/public/board-14.svg'
import Board15 from '@/public/board-15.svg'
import Board16 from '@/public/board-16.svg'
import Board17 from '@/public/board-17.svg'
import Board18 from '@/public/board-18.svg'
import { useEffect, useState } from "react";
import Image from "next/image";
import PuzzleItem from "@/app/components/PuzzleItem";
import Se from '@/public/se.svg'
import Apertar from '@/public/apertar.svg'
import MostrarImagem from '@/public/mostrar_imagem.svg'
import TocarSom from '@/public/tocar_som.svg'
import E from '@/public/e.svg'
import EIrPara from '@/public/e_ir_para.svg'
import EFimDaTerapia from '@/public/e_fim_da_terapia.svg'
import Quadrante from '@/public/quadrante.svg'

export default function Terapy() {

	const [activeQuadrant, setActiveQuadrant] = useState(0);
	const [isToggleOn, setToggle] = useState(false);
	const [therapyName, setTherapyName] = useState("Terapia 1");
	const [isEditing, setIsEditing] = useState(false);
	const [droppedItems, setDroppedItems] = useState<Array<{icon: any, commandName: string, pairableItems: string[]}>>([]);


	const handleQuadrantClick = (index : number) => {
		setActiveQuadrant(index);
	};

	const handleToggleClick = () => {
		setToggle(!isToggleOn);
	};

	const handleNameClick = () => {
		setIsEditing(true);
	};
	
	const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setTherapyName(event.target.value);
	};
	
	const handleNameBlur = () => {
		setIsEditing(false);
	};

	const handleDragOver = (event: React.DragEvent) => {
		event.preventDefault(); // Necessary to allow drop.
	};

	const handleDrop = async (event: React.DragEvent) => {
		event.preventDefault();
		const data = JSON.parse(event.dataTransfer.getData("text/plain"));
		
		if (droppedItems.length > 0) {
			const lastDroppedItem = droppedItems[droppedItems.length - 1];
			if (!lastDroppedItem.pairableItems.includes(data.commandName)) {
				return;
			}
		}

		setDroppedItems(prevItems => [...prevItems, data]);
	};

	let rawCode : string;

	useEffect(() => {
		rawCode = `programa "${therapyName}":\n`
		rawCode += 'inicio\n'
		rawCode += 'quadrante = ler()\n'

		droppedItems.forEach((item, index) => {
			item.commandName === 'Se' && (rawCode += 'se ')
			item.commandName === 'Apertar' && (rawCode += `quadrante == ${activeQuadrant} entao inicio\n`)
			item.commandName === 'Mostrar imagem' && (rawCode += `mostrar()`)
			item.commandName === 'E' && (rawCode += `\n`)
			item.commandName === 'Tocar som' && (rawCode += `tocar()`)
			item.commandName === 'E fim' && (rawCode += `\nfim\n`)
		})

		rawCode += 'fim.\n'

		if(droppedItems.some(e => e.commandName === 'E fim')) {
			console.log(rawCode);
		}



	}, [therapyName, droppedItems])
	

	const boardIcons = [Board1, Board2, Board3, Board4, Board5, Board6, Board7, Board8, Board9, Board10, Board11, Board12, Board13, Board14, Board15, Board16, Board17, Board18];

	const chunkedBoardIcons = [];
	for (let i = 0; i < boardIcons.length; i += 3) {
		chunkedBoardIcons.push(boardIcons.slice(i, i + 3));
	}


	return (
		<div className='flex flex-col w-full h-[100vh]'>
			<div className='flex justify-between items-center p-6 w-full h-[10%] bg-[#F8F8F8] border-b-2 border-[#EFEFEF]'>
				<div className='flex gap-4'>
					<div className='flex gap-2'>
						<a className='hover:underline hover:duration-300 hover:scale-110' href='/dashboard/therapies'>Terapias</a>
						<span> {'>'} </span>
						{isEditing ? (
							<input
								type="text"
								value={therapyName}
								onChange={handleNameChange}
								onBlur={handleNameBlur}
								autoFocus
								className='rounded-sm border-[1px] border-solid border-[#E6E6EB] px-4 text-sm font-normal'
							/>
						) : (
							<span onClick={handleNameClick}> {therapyName} </span>
						)}
					</div>
					<Image className='hover:scale-125 duration-300 cursor-pointer w-auto h-auto' src={Trash} alt='Excluir' />
				</div>
				<div className='flex gap-8 items-center'>
					<p>Última modificação agora</p>

					<div className='flex gap-2 items-center cursor-pointer' onClick={handleToggleClick}>
						<div className='relative'>
							<div className={`block transition-colors duration-300 ${isToggleOn ? 'bg-[#E7343F]' : 'bg-[#b9b9b9]'} w-14 h-8 rounded-full`}></div>
							<div className={`dot absolute transition-all duration-600 ${isToggleOn ? 'right-1' : 'left-1'} top-1 bg-[#EFEFEF] w-6 h-6 rounded-full`}></div>
						</div>
						<div className="ml-3 text-gray-700 font-medium">
							{isToggleOn ? 'Público' : 'Privado'}
						</div>
					</div>


					<div className='w-40'>
						<ButtonMin text='Começar' icon={Play}></ButtonMin>
					</div>
				</div>
				
			</div>
			<div className='flex w-full h-[100%]'>
				<div className='flex flex-col w-[78%] h-[100%]'>
					{/* Draggable items */}
					<div className='flex justify-start items-center px-12 py-3 w-full h-[30%] border-b-2 border-[#EFEFEF] gap-12'>
							<PuzzleItem icon={Se} commandName='Se' pairableItems={['Apertar']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={Apertar} commandName='Apertar' pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={MostrarImagem} commandName='Mostrar imagem' pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={TocarSom} commandName='Tocar som' pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={E} commandName='E' pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={EIrPara} commandName='E ir para' pairableItems={['Quadrante']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={EFimDaTerapia} commandName='E fim' pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
							<PuzzleItem icon={Quadrante} commandName='Quadrante' pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
					</div>
					<div id='o' className='w-full h-[70%] flex justify-center items-center gap-8' onDragOver={handleDragOver} onDrop={handleDrop}>
						{/* Dropped items */}
						{droppedItems.map((item, index) => (
							<div key={index} className='flex flex-col justify-center  items-center gap-3'>
								<Image src={item.icon} alt={item.commandName} />
								<span className='text-black text-base font-light'>{item.commandName}</span>
							</div>
						))}
					</div>
				</div>
				<div className='flex flex-col bg-[#EEEEEE] w-[22%] h-[100%] items-center justify-center gap-4'>
					{chunkedBoardIcons.map((chunk, i) => (
						<div className='flex gap-4' key={i}>
							{chunk.map((icon, j) => (
								<div key={j} onClick={() => handleQuadrantClick(i * 3 + j)}>
									<BoardQuadrant icon={icon} active={activeQuadrant === i * 3 + j} />
								</div>
							))}
						</div>
					))}
				</div>
			</div>
		</div>
	)
}