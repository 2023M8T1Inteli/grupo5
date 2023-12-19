import React from 'react';
import PuzzleItem from "@/app/components/PuzzleItem";

interface DraggableItemProps {
	icon: any, 
	commandName: string, 
	pairableItems: string[], 
	lastDroppedItem: {icon: any, commandName: string, pairableItems: string[]} | null 
}

const DraggableItem: React.FC<DraggableItemProps> = ({ icon, commandName, pairableItems, lastDroppedItem }) => {
	return (
		<PuzzleItem 
			icon={icon} 
			commandName={commandName} 
			pairableItems={pairableItems} 
			lastDroppedItem={lastDroppedItem} 
		/>
	);
}

export default DraggableItem;
