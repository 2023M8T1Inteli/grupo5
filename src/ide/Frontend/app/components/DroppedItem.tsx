import React from 'react';
import Image from 'next/image';

interface DroppedItemProps {
    item: {
        icon: any, 
        commandName: string, 
        pairableItems: string[]
    }, 
    index: number
}

const DroppedItem: React.FC<DroppedItemProps> = ({ item, index }) => {
    return (
        <div key={index} className='flex flex-col justify-center  items-center gap-3'>
            <Image src={item.icon} alt={item.commandName} />
            <span className='text-black text-base font-light'>{item.commandName}</span>
        </div>
    );
}

export default DroppedItem;
