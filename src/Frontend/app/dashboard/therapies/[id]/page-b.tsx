'use client'
import { useState, useEffect, useRef } from 'react';

// Custom hooks
import useToggle from '@/app/hooks/useToggle';
import useTherapyName from '@/app/hooks/useTherapyName';
import useDrop from '@/app/hooks/useDrop';

// Components
import Image from "next/image";
import DraggableItem from '@/app/components/DraggableItem';
import DroppedItem from '@/app/components/DroppedItem';

// Icons
import Se from '@/public/se.svg'
import Apertar from '@/public/apertar.svg'
import MostrarImagem from '@/public/mostrar_imagem.svg'
import TocarSom from '@/public/tocar_som.svg'
import E from '@/public/e.svg'
import EIrPara from '@/public/e_ir_para.svg'
import EFimDaTerapia from '@/public/e_fim_da_terapia.svg'
import Quadrante from '@/public/quadrante.svg'
import ButtonMin from '@/app/components/ButtonMin';
import BoardQuadrant from "@/app/components/BoardQuadrant";
import Play from '@/public/play_white.svg'
import Trash from '@/public/trash.svg'
import * as Boards from '@/public/boards';
import PuzzleItem from '@/app/components/PuzzleItem';

export default function Terapy() {
	const [activeQuadrant, setActiveQuadrant] = useState(0);
	const [isToggleOn, handleToggleClick] = useToggle(false);
	const [therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur] = useTherapyName("Terapia 1");
	const [droppedItems, handleDragOver, handleDrop] = useDrop([]);

	const handleQuadrantClick = (index : number) => {
		setActiveQuadrant(index);
	};

	const commandNameMapping: { [key: string]: string } = {
		'Se': 'se ',
		'Apertar': `quadrante == ${activeQuadrant} entao inicio\n`,
		'Mostrar imagem': `mostrar()`,
		'E': `\n`,
		'Tocar som': `tocar()`,
		'E fim': `\nfim\n`
	}

	const rawCode = useRef('');

	useEffect(() => {
		rawCode.current = `programa "${therapyName}":\n`;
		rawCode.current += 'inicio\n';
		rawCode.current += 'quadrante = ler()\n';
	
		droppedItems.forEach((item, index) => {
			rawCode.current += commandNameMapping[item.commandName] || '';
		});
	
		rawCode.current += 'fim.\n';
	
		if (droppedItems.some(e => e.commandName === 'E fim')) {
			console.log(rawCode.current);
		}
	// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [therapyName, droppedItems]);

	const boardIcons = Object.values(Boards);

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
						<DraggableItem icon={Se} commandName='Se' pairableItems={['Apertar']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={Apertar} commandName='Apertar' pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={MostrarImagem} commandName='Mostrar imagem' pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={TocarSom} commandName='Tocar som' pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={E} commandName='E' pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={EIrPara} commandName='E ir para' pairableItems={['Quadrante']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={EFimDaTerapia} commandName='E fim' pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
						<DraggableItem icon={Quadrante} commandName='Quadrante' pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
					</div>
					<div id='o' className='w-full h-[70%] flex justify-center items-center gap-8' onDragOver={handleDragOver} onDrop={handleDrop}>
						{/* Dropped items */}
						{droppedItems.map((item, index) => (
							<DroppedItem key={index} item={item} index={index} />
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
