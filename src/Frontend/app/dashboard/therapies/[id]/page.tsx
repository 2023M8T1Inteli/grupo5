'use client'

// Libraries
import { useState, useEffect, useRef, useMemo } from 'react';

// Hooks
import useDrop from '@/app/hooks/useDrop';
import useToggle from '@/app/hooks/useToggle';
import useTherapyName from '@/app/hooks/useTherapyName';

// Components
import BoardIcons from '@/app/components/BoardIcons';
import DroppedItems from '@/app/components/DroppedItems';
import TherapyHeader from '@/app/components/TherapyHeader';
import DraggableItems from '@/app/components/DraggableItems';

// Helpers and constants
import * as Boards from '@/public/boards';
import chunkArray from '@/app/helpers/chuckArray';
import { commandNameMapping, generateRawCode } from '@/app/helpers/codeGenerator';

export default function Terapy() {
    const [activeQuadrant, setActiveQuadrant] = useState(0);
    const [isToggleOn, handleToggleClick] = useToggle(false);
    const [therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur] = useTherapyName("Terapia 1");
    const [droppedItems, handleDragOver, handleDrop] = useDrop([]);

    const handleQuadrantClick = (index : number) => {
        setActiveQuadrant(index);
    };

    useEffect(() => {
        generateRawCode(therapyName, droppedItems, commandNameMapping(activeQuadrant));
    }, [therapyName, droppedItems, activeQuadrant]);

	const boardIcons = Object.values(Boards);
    const chunkedBoardIcons = useMemo(() => chunkArray(boardIcons, 3), [boardIcons]);

    return (
        <div className='flex flex-col w-[85%] h-[100vh]'>
            <TherapyHeader {...{ therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur, isToggleOn, handleToggleClick }} />
            <div className='flex w-full h-[100%]'>
                <div className='flex flex-col w-[78%] h-[100%]'>
                    <DraggableItems droppedItems={droppedItems} />
                    <DroppedItems {...{ droppedItems, handleDragOver, handleDrop }} />
                </div>
                <BoardIcons {...{ handleQuadrantClick, activeQuadrant, chunkedBoardIcons }} />
            </div>
        </div>
    )
}

