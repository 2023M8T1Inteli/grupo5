import React from 'react';
import PuzzleItem from "@/app/components/PuzzleItem";

interface DraggableItemProps {
	icon: any, 
	commandName: string, 
	pairableItems: string[],
	blockType: blockType, 
	lastDroppedItem: {icon: any, commandName: string, pairableItems: string[], blockType: string} | null 
} 
export enum blockType {
	Conditional,
	Action,
	Asset,
} 

const DraggableItem: React.FC<DraggableItemProps> = ({ icon, commandName, pairableItems, lastDroppedItem, blockType }) => {
	return (
		<PuzzleItem 
			icon={icon} 
			commandName={commandName} 
			pairableItems={pairableItems} 
			lastDroppedItem={lastDroppedItem} 
			blockType={blockType.toString()} 
		/>
	);
}

export default DraggableItem;
