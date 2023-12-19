import DraggableItem, { blockType } from './DraggableItem';
import Se from '@/public/se.svg'
import Apertar from '@/public/apertar.svg'
import MostrarImagem from '@/public/mostrar_imagem.svg'
import TocarSom from '@/public/tocar_som.svg'
import E from '@/public/e.svg'
import EIrPara from '@/public/e_ir_para.svg'
import EFimDaTerapia from '@/public/e_fim_da_terapia.svg'
import Quadrante from '@/public/quadrante.svg'
import { IDroppedItem } from '../hooks/useDrop';

export default function DraggableItems({ droppedItems } : { droppedItems: IDroppedItem[] }) {
    return (
        <div className='flex justify-start items-center px-12 py-3 w-full h-[30%] border-b-2 border-[#EFEFEF] gap-12'>
            <DraggableItem icon={Se} commandName='Se' blockType = {blockType.Conditional} pairableItems={['Apertar']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={Apertar} commandName='Apertar' blockType = {blockType.Action} pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={MostrarImagem} commandName='Mostrar imagem' blockType = {blockType.Asset} pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={TocarSom} commandName='Tocar som' blockType = {blockType.Asset} pairableItems={['E', 'E ir para', 'E fim']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={E} commandName='E' blockType = {blockType.Action} pairableItems={['Mostrar imagem', 'Tocar som']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={EIrPara} commandName='E ir para' blockType = {blockType.Action} pairableItems={['Quadrante']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={EFimDaTerapia} commandName='E fim' blockType = {blockType.Action} pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
            <DraggableItem icon={Quadrante} commandName='Quadrante' blockType = {blockType.Action} pairableItems={['']} lastDroppedItem={droppedItems[droppedItems.length - 1] || null} />
        </div>
    )
}