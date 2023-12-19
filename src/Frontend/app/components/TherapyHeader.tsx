import Image from "next/image";
import ButtonMin from "./ButtonMin";
import Trash from "@/public/trash.svg"
import Play from "@/public/play_white.svg"
import React, { useCallback } from 'react';


export default function TherapyHeader({ therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur, isToggleOn, handleToggleClick, onClickTrashIcon } : { therapyName: string, isEditing: boolean, handleNameClick: () => void, handleNameChange: (e: React.ChangeEvent<HTMLInputElement>) => void, handleNameBlur: () => void, isToggleOn: boolean, handleToggleClick: () => void, onClickTrashIcon: () => void }) {
    const iniciarConexaoWebSocket = async () => {
        try {
            // Faça uma solicitação POST para iniciar a conexão WebSocket
            const response = await fetch('http://127.0.0.1:3001/start_websocket', { method: 'POST' });
    
            if (response.ok) {
                console.log('Servidor WebSocket iniciado com sucesso!');
    
                // Adicione lógica para criar uma conexão WebSocket
                const websocket = new WebSocket('ws://localhost:8765');
    
                // Adicione um ouvinte para eventos de abertura da conexão WebSocket
                websocket.addEventListener('open', () => {
                    console.log('Conexão WebSocket aberta com sucesso!');
                });
    
                // Adicione um ouvinte para eventos de mensagem
                websocket.addEventListener('message', (event) => {
                    console.log('Mensagem recebida:', event.data);
                    const quadrante = event.data;
                    console.log('Quadrante tocado pelo cliente:', quadrante);
                    
                    // Adicione aqui a lógica para manipular o quadrante recebido, se necessário
                });
    
                // Adicione um ouvinte para eventos de erro na conexão WebSocket
                websocket.addEventListener('error', (event) => {
                    console.error('Erro na conexão WebSocket:', event);
                });
    
                // Adicione um ouvinte para eventos de fechamento da conexão WebSocket
                websocket.addEventListener('close', (event) => {
                    console.log('Conexão WebSocket fechada:', event);
                });
            } else {
                console.error('Erro ao iniciar o servidor WebSocket.');
            }
        } catch (error) {
            console.error('Erro:', error);
        }
    };
    
    
      
    return (
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
                <Image className='hover:scale-125 duration-300 cursor-pointer w-auto h-auto' src={Trash} alt='Excluir' onClick={onClickTrashIcon}/>
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
                <ButtonMin text='Compilar' icon={Play} onClick={iniciarConexaoWebSocket}></ButtonMin>
                </div>
            </div>
        </div>
    )
}